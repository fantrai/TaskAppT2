using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;

namespace TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet
{
    partial class CardSetPanVM : ObservableObject
    {
        public CardSetPanVM(LocalDataBase db)
        {
            this.db = db;
            OnUseCardSet += UseCardSet;
        }

        ~CardSetPanVM()
        {
            OnUseCardSet -= UseCardSet;
        }

        public static Action<CardSet>? OnUseCardSet {  get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Card> Cards { get; set; } = [];

        [ObservableProperty]
        public partial string NameSet { get; set; } = string.Empty;

        readonly LocalDataBase db;
        CardSet? useSet;

        void UseCardSet(CardSet set)
        {
            useSet = set;
            Cards = useSet.Cards.ToObservableCollection();
            NameSet = useSet.Name;
        }
    }
}
