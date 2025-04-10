using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2._Views;
using TaskAppT2._Views.Study.FlashCards;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._ViewModels.Study.FlashCards
{
    partial class NewCathegoryPageVM : ObservableObject
    {
        public NewCathegoryPageVM(LocalDataBase db)
        {
            this.db = db;
            ResetPan();
            db.SavedChanges += UpdateCardSets;
            OnResetCathegoryPan += ResetPan;
            OnUseCathegory += UseCathegory;
            NewCathegoryPage.OnChangeSetCathegory += UpdateSetCathegory;
        }

        ~NewCathegoryPageVM()
        {
            OnUseCathegory -= UseCathegory;
            OnResetCathegoryPan -= ResetPan;
            NewCathegoryPage.OnChangeSetCathegory -= UpdateSetCathegory;
        }

        public static Action<CardSetCathegory>? OnUseCathegory { get; set; }
        public static Action? OnResetCathegoryPan { get; set; }

        [ObservableProperty]
        public partial string NameCathegory { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ObservableCollection<CardSet> CardSet { get; set; } = [];

        readonly LocalDataBase db;
        CardSetCathegory Cathegory = null!;

        private void ResetPan()
        {
            Cathegory = new CardSetCathegory(string.Empty);
            NameCathegory = string.Empty;
            UpdateCardSets(null, null!);
        }

        void UseCathegory(CardSetCathegory cathegory)
        {
            Cathegory = cathegory;
            NameCathegory = cathegory.Name;
            UpdateCardSets(null, null!);
        }

        void UpdateCardSets(object? sender, SavedChangesEventArgs e)
        {
            CardSet = db.CardSets.Where(s => s.CardSetCathegory == null || s.CardSetCathegory == Cathegory).ToObservableCollection();
        }

        [RelayCommand]
        async Task SaveChangeCathegory()
        {
            if (NameCathegory == string.Empty)
            {
                PopUI.ShowSnackErr("Введите название папки!");
                return;
            }
            else if (NameCathegory.Equals("все наборы", StringComparison.CurrentCultureIgnoreCase)
                ||  db.CardSetsCathegorys.Any(c => c.Name.ToLower() == NameCathegory.ToLower()) 
                && !db.CardSetsCathegorys.Contains(Cathegory))
            {
                PopUI.ShowSnackErr("Название уже занято папки!");
                return;
            }

            FlashCardsPan.OnStartGoBack?.Invoke();
            Cathegory.Name = NameCathegory;

            if (!db.CardSetsCathegorys.Contains(Cathegory))
                await db.CardSetsCathegorys.AddAsync(Cathegory);
            await db.SaveChangesAsync();
            db.CardSets.UpdateRange(CardSet);
            ResetPan();
        }

        [RelayCommand]
        async Task DeleteCathegory()
        {
            ContinuePage.ActionWithContinue += async () => 
            { 
                if (db.CardSetsCathegorys.Contains(Cathegory)) 
                { 
                    db.CardSetsCathegorys.Remove(Cathegory); 
                    await db.SaveChangesAsync();
                    await Shell.Current.Navigation.PopModalAsync(false);
                    await Shell.Current.Navigation.PopModalAsync(false);
                } 
            };
            await ContinuePage.OpenPan("Удаление папки", $"Вы уверены, что хотите удалить папку \"{ NameCathegory }\"?", UseImage.Trash);
        }

        void UpdateSetCathegory(CardSet set, bool val)
        {
            if (val)
            {
                set.CardSetCathegory = Cathegory;
            }
            else
            {
                set.CardSetCathegory = null;
            }
        }
    }
}
