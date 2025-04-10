using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2._Views.Study.FlashCards;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._ViewModels.Study.FlashCards
{
    partial class NewCardSetPanVM : ObservableObject
    {
        public NewCardSetPanVM(LocalDataBase db)
        {
            this.db = db;
            ResetPan();
            OnUseCardSet += UseSet;
            NewCardSetPan.OnRemoveCard += RemoveCard;
            FlashCardsPan.OnStartGoBack += ResetPan;
            NewCardSetPan.OnCreateNewCard += AddNewCard;
        }

        ~NewCardSetPanVM()
        {
            OnUseCardSet -= UseSet;
            NewCardSetPan.OnRemoveCard -= RemoveCard;
            FlashCardsPan.OnStartGoBack -= ResetPan;
            NewCardSetPan.OnCreateNewCard -= AddNewCard;
        }

        public static Action<CardSet>? OnUseCardSet { get; set; }

        [ObservableProperty]
        public partial string SetName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ObservableCollection<Card> Cards { get; set; } = null!;

        readonly LocalDataBase db;

        CardSet? updatingCardSet = null;

        void UseSet(CardSet set)
        {
            updatingCardSet = set;
            Cards = set.Cards.ToObservableCollection();
            SetName = set.Name;
        }

        void ResetPan()
        {
            updatingCardSet = null;
            Cards = [new Card(string.Empty, string.Empty)];
            SetName = string.Empty;
        }

        void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }

        [RelayCommand]
        async Task SaveChanges()
        {
            if (SetName == string.Empty)
            {
                PopUI.ShowSnackErr("Введите имя набора!");
                return;
            }
            else if (Cards.Count < 2)
            {
                PopUI.ShowSnackErr("В наборе должно быть не менее 2-х карточек!");
                return;
            }
            else if (Cards.Last().Term == string.Empty
                || Cards.Last().Description == string.Empty)
            {
                PopUI.ShowSnackErr("Вы не закончили заполнять последнюю карточку!");
                return;
            }
            else if (!db.CardSets.Contains(updatingCardSet) 
                && db.CardSets.Any(c => c.Name.ToLower() == SetName.ToLower()))
            {
                PopUI.ShowSnackErr("Имя набора уже занято!");
                return;
            }

            if (updatingCardSet == null)
            {
                CardSet save;
                save = new CardSet(SetName)
                {
                    Cards = [.. Cards],
                    Name = SetName
                };
                await db.CardSets.AddAsync(save);
            }
            else
            {
                updatingCardSet.Cards = [.. Cards];
                updatingCardSet.Name = SetName;
                db.Update(updatingCardSet);
            }
            FlashCardsPan.OnStartGoBack?.Invoke();
            await db.SaveChangesAsync();
        }

        void AddNewCard()
        {
            if (Cards.Count > 0 
                && (Cards.Last().Term == string.Empty 
                || Cards.Last().Description == string.Empty))
            {
                PopUI.ShowSnackErr("Вначале заполните прошлую карточку!");
                return;
            }

            Cards.Add(new Card(string.Empty, string.Empty));
        }
    }
}
