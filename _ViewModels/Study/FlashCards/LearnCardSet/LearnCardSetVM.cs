using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2.MainMenu.Study.FlashCards;

namespace TaskAppT2._ViewModels.Study.FlashCards.LearnCardSet
{
    partial class LearnCardSetVM : ObservableObject
    {
        public LearnCardSetVM()
        {
            OnUseCards += UseCards;
            OnReplayLernSet += () => { UseCards(allCards); };
            OnCheckCross += CheckCross;
        }

        ~LearnCardSetVM()
        {
            OnUseCards -= UseCards;
            OnCheckCross -= CheckCross;
        }

        public static Action<List<Card>>? OnUseCards { get; set; }
        public static Action? OnGoResultLern { get; set; }
        public static Action? OnReplayLernSet { get; set; }
        public static Action<string, string>? OnCheckCross { get; set; }

        [ObservableProperty]
        public partial string ProgressText { get; set; } = string.Empty;

        [ObservableProperty]
        public partial int Failed { get; set; } = 0;

        [ObservableProperty]
        public partial int ResultFailed { get; set; } = 0;

        [ObservableProperty]
        public partial int Sucsess { get; set; } = 0;

        [ObservableProperty]
        public partial int ResultSucess { get; set; } = 0;

        [ObservableProperty]
        public partial double Progress { get; set; } = 0;

        [ObservableProperty]
        public partial string TextCard { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Card ThisCard { get; set; } = null!;

        [ObservableProperty]
        public partial ObservableCollection<string> RandomDescriptions { get; set; } = [];

        [ObservableProperty]
        public partial string ResultPerProcient { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string TimerText { get; set; } = "0";

        [ObservableProperty]
        public partial ObservableCollection<CrossCardView> CrossCards { get; set; } = [];

        List<Card> allCards = [];
        List<Card> cards = [];
        int maxCountCards = 0;
        readonly Random random = new();
        int CountLearn { get { return Failed + Sucsess; } }
        System.Timers.Timer? timer;
        float lateTime = 0;

        void UseCards(List<Card> cards)
        {
            allCards = [.. cards];
            this.cards = [.. cards];
            maxCountCards = cards.Count;
            ResetTime();
            Failed = 0;
            Sucsess = 0;
            Progress = (double)Failed / (CountLearn == 0 ? 1 : CountLearn);
            ProgressText = (maxCountCards - cards.Count) + "/" + maxCountCards;
            GetRandomNextCard();
        }

        void GetRandomNextCard()
        {
            ThisCard = cards[random.Next(cards.Count)];
            TextCard = ThisCard.Term;

            RandomDescriptions.Clear();
            RandomDescriptions.Add(ThisCard.Description);
            for (int i = 0; i < allCards.Count - 1 && i < 2; i++)
            {
                var randtext = allCards[random.Next(allCards.Count)].Description;
                if (RandomDescriptions.Contains(randtext))
                {
                    i--;
                    continue;
                }
                RandomDescriptions.Add(randtext);
            }
            var list = RandomDescriptions.ToList();
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(list));
            RandomDescriptions = list.ToObservableCollection();

            cards.Remove(ThisCard);
        }

        [RelayCommand]
        void RotateCard()
        {
            TextCard = TextCard == ThisCard.Term ? ThisCard.Description : ThisCard.Term;
        }

        [RelayCommand]
        void GoNexCard(bool resurlt)
        {
            ProgressText = (maxCountCards - cards.Count) + "/" + maxCountCards;
            if (resurlt)
                Sucsess++;
            else
                Failed++;
            Progress = (double)Failed / (CountLearn == 0 ? 1 : CountLearn);

            if (cards.Count > 0)
                GetRandomNextCard();
            else
            {
                OpenResultPan();
            }
        }

        void OpenResultPan()
        {
            Progress = (double)Failed / (CountLearn == 0 ? 1 : CountLearn);
            OnGoResultLern?.Invoke();
            ResultSucess = (int)((1 - Progress) * 150);
            ResultFailed = (int)(Progress * 150);
            ResultPerProcient = (int)((1 - Progress) * 100) + "%";
        }

        [RelayCommand]
        void CheckRequest(string? request)
        {
            if (request == null)
            {
                GoNexCard(false);
                return;
            }
            GoNexCard(request.Equals(ThisCard.Term, StringComparison.CurrentCultureIgnoreCase));
        }

        [RelayCommand]
        void CheckRequestDescr(string request)
        {
            if (request == null)
            {
                GoNexCard(false);
                return;
            }
            GoNexCard(request.Equals(ThisCard.Description, StringComparison.CurrentCultureIgnoreCase));
        }

        [RelayCommand]
        void StartCrossing()
        {
            NewCrossCard();
            timer = new System.Timers.Timer(100);
            timer.Elapsed += TimerUpdate;
            timer.Start();
            FlashCardsPan.OnStartGoBack += StopTimer;
            FlashCardsPan.OnStartGoBack += ResetTime;
        }

        private void TimerUpdate(object? sender, ElapsedEventArgs e)
        {
            lateTime += 0.1f;
            TimerText = string.Format("{0:f1}", lateTime) + " секунд";
        }

        void StopTimer()
        {
            timer?.Stop();
            FlashCardsPan.OnStartGoBack -= StopTimer;
        }

        void ResetTime()
        {
            lateTime = 0;
            TimerText = string.Format("{0:f1}", lateTime) + " секунд";
            FlashCardsPan.OnStartGoBack -= ResetTime;
        }

        void NewCrossCard()
        {
            CrossCards.Clear();
            CrossCards.Add(new CrossCardView { Text = ThisCard.Term, IsTerm = true });
            CrossCards.Add(new CrossCardView { Text = ThisCard.Description, IsTerm = false });
            for (int i = 0; i < 6 && cards.Count > 0; i++)
            {
                GetRandomNextCard();
                CrossCards.Add(new CrossCardView { Text = ThisCard.Term, IsTerm = true });
                CrossCards.Add(new CrossCardView { Text = ThisCard.Description, IsTerm = false });
            }
            var list = CrossCards.ToList();
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(list));
            CrossCards = list.ToObservableCollection();
        }

        private void CheckCross(string str1, string str2)
        {
            if (allCards.Find(c => (c.Term == str1 && c.Description == str2) || (c.Term == str2 && c.Description == str1)) != null) 
            {
                Sucsess++;
                CrossCards.Remove(CrossCards.First(c => c.Text == str1));
                CrossCards.Remove(CrossCards.First(c => c.Text == str2));
                if (CrossCards.Count == 0)
                {
                    if (cards.Count != 0)
                    {
                        NewCrossCard();
                    }
                    else
                    {
                        StopTimer();
                        OpenResultPan();
                        ProgressText = TimerText;
                    }
                }
            }
            else
            {
                Failed++;
            }
            Progress = (double)Failed / (CountLearn == 0 ? 1 : CountLearn);
        }
    }

    class CrossCardView()
    {
        public string Text { get; set; } = string.Empty;
        public bool IsTerm { get; set; } = false;
    }
}