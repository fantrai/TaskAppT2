using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2._Views;
using TaskAppT2._Views.Study.Subjects;

namespace TaskAppT2._ViewModels.Study.Subjects
{
    partial class MainPageSubjectsPanVM : ObservableObject
    {
        public MainPageSubjectsPanVM(LocalDataBase db)
        {
            this.db = db;
            db.SavedChanges += (s, e) => { SetData(); };
            SetData();
            MainPageSubjectsPan.OnDeleteSubject += DeleteSubject;
            MainPageSubjectsPan.OnFindText += UpdateFindText;
        }

        ~MainPageSubjectsPanVM()
        {
            MainPageSubjectsPan.OnDeleteSubject -= DeleteSubject;
            MainPageSubjectsPan.OnFindText -= UpdateFindText;
        }

        [ObservableProperty]
        public partial ObservableCollection<Subject> Subjects { get; set; } = null!;

        readonly LocalDataBase db;
        string findText = string.Empty;

        void SetData()
        {
            Subjects = db.Subjects.ToObservableCollection();
            Subjects = Subjects.Where(cs => cs.Name.StartsWith(findText, StringComparison.OrdinalIgnoreCase)).ToObservableCollection();
        }

        void DeleteSubject(Subject sub)
        {
            ContinuePage.ActionWithContinue += async () => {
                db.Subjects.Remove(sub);
                await db.SaveChangesAsync();
            };
            var _ = ContinuePage.OpenPan("Удаление предмета", $"Вы уверены, что хотите удалить предмет {sub.Name}?", UseImage.Trash);
        }

        void UpdateFindText(string text)
        {
            findText = text;
            SetData();
        }
    }
}
