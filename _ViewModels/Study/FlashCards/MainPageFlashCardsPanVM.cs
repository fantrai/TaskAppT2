using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
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
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._ViewModels.Study.FlashCards
{
    partial class MainPageFlashCardsPanVM : ObservableObject
    {
        public MainPageFlashCardsPanVM(LocalDataBase db)
        {
            this.db = db;
            db.SavedChanges += (s, e) => { SetData(); };
            SetData();
            MainPageFlashCardsPan.OnDeleteCardSet += DeleteCardSet;
            MainPageFlashCardsPan.OnChangeSelectCathegory += UpdateCardSetCathegory;
            MainPageFlashCardsPan.OnFindText += UpdateFindText;
        }

        ~MainPageFlashCardsPanVM()
        {
            MainPageFlashCardsPan.OnDeleteCardSet -= DeleteCardSet;
            MainPageFlashCardsPan.OnChangeSelectCathegory -= UpdateCardSetCathegory;
            MainPageFlashCardsPan.OnFindText -= UpdateFindText;
        }

        [ObservableProperty]
        public partial ObservableCollection<CardSet> CardSets { get; set; } = null!;
        [ObservableProperty]
        public partial ObservableCollection<CardSetCathegory> CardSetCathegories { get; set; } = null!;

        readonly LocalDataBase db;
        CardSetCathegory? selectionCathegory;
        string findText = string.Empty;

        void SetData()
        {
            if (selectionCathegory == null)
            {
                CardSets = db.CardSets.ToObservableCollection();
            }
            else
            {
                CardSets = db.CardSets.Where(cs => cs.CardSetCathegory == selectionCathegory).ToObservableCollection();
            }
            CardSets = CardSets.Where(cs => cs.Name.StartsWith(findText, StringComparison.OrdinalIgnoreCase)).ToObservableCollection();
            CardSetCathegories = db.CardSetsCathegorys.ToObservableCollection();
        }

        void DeleteCardSet(CardSet set)
        {
            ContinuePage.ActionWithContinue += async () => { 
                db.CardSets.Remove(set); 
                await db.SaveChangesAsync(); 
            };
            var _ = ContinuePage.OpenPan("Удаление набора", $"Вы уверены, что хотите удалить набор {set.Name}? Все карточки данного набора также будут удалены!", UseImage.Trash);
        }

        void UpdateCardSetCathegory(CardSetCathegory? cathegory)
        {
            selectionCathegory = cathegory;
            SetData();
        }

        void UpdateFindText(string text)
        {
            findText = text;
            SetData();
        }
    }
}
