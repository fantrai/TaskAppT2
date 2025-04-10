using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppT2._Models;
using TaskAppT2._Servise;
using TaskAppT2._Views.Study.Subjects;

namespace TaskAppT2._ViewModels.Study.Subjects
{
    partial class NewSubjectPanVM : ObservableObject
    {
        public NewSubjectPanVM(LocalDataBase db)
        {
            this.db = db;
            ResetPan();
            OnUseSubject += UseSubject;
            SubjectsPan.OnStartGoBack += ResetPan;
            OnSelectTeacher += SelectTeacher;
        }

        ~NewSubjectPanVM()
        {
            OnUseSubject -= UseSubject;
            SubjectsPan.OnStartGoBack -= ResetPan;
            OnSelectTeacher -= SelectTeacher;
        }

        public static Action<Subject>? OnUseSubject { get; set; }
        public static Func<Task>? OnGoSelectTeacher { get; set; }
        public static Action<Teacher?>? OnSelectTeacher { get; set; }

        [ObservableProperty]
        public partial string SubjName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string? Room { get; set; }

        [ObservableProperty]
        public partial Teacher? Teacher { get; set; } = null;

        [ObservableProperty]
        public partial string? Note { get; set; } = string.Empty;

        readonly LocalDataBase db;

        Subject? updatingSubject = null;

        void UseSubject(Subject subj)
        {
            updatingSubject = subj;
            SubjName = subj.Name;
            Room = subj.Room;
            Note = subj.Note;
            Teacher = subj.Teacher;
        }

        void ResetPan()
        {
            updatingSubject = null;
            SubjName = string.Empty;
            Room = null;
            Teacher = null;
            Note = null;
        }

        [RelayCommand]
        async Task SaveChanges()
        {
            if (SubjName == string.Empty)
            {
                PopUI.ShowSnackErr("Введите имя предмета!");
                return;
            }
            else if (!db.Subjects.Contains(updatingSubject)
                && db.Subjects.Any(s => s.Name.ToLower() == SubjName.ToLower()))
            {
                PopUI.ShowSnackErr("Имя предмета уже занято!");
                return;
            }

            if (updatingSubject == null)
            {
                Subject save;
                save = new Subject(SubjName)
                {
                    Teacher = Teacher,
                    Note = Note,
                    Room = Room,
                };
                await db.Subjects.AddAsync(save);
            }
            else
            {
                updatingSubject.Teacher = Teacher;
                updatingSubject.Name = SubjName;
                updatingSubject.Room = Room;
                updatingSubject.Note = Note;
                db.Update(updatingSubject);
            }
            SubjectsPan.OnStartGoBack?.Invoke();
            await db.SaveChangesAsync();
        }

        [RelayCommand]
        void OpenSelectTeacher()
        {
            OnGoSelectTeacher?.Invoke();
        }

        private void SelectTeacher(Teacher? teacher)
        {
            Teacher = teacher;
        }
    }
}
