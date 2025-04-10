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
using TaskAppT2._Views.Study.Subjects;

namespace TaskAppT2._ViewModels.Study.Subjects
{
    partial class SelectTeacherPageVM : ObservableObject
    {
        public SelectTeacherPageVM(LocalDataBase db)
        {
            this.db = db;
            db.SavedChanges += UpdateTeachers;
        }

        ~SelectTeacherPageVM()
        {
            db.SavedChanges -= UpdateTeachers;
        }

        public Action<Subject>? OnUseSubject { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Teacher>? Teachers { get; set; }

        readonly LocalDataBase db;

        void UpdateTeachers(object? sender, SavedChangesEventArgs e)
        {
            Teachers = db.Teachers.ToObservableCollection();
            if (Teachers.Count == 0) Teachers = null;
        }

        [RelayCommand]
        void SelectTeacher(Teacher? teacher)
        {
            NewSubjectPanVM.OnSelectTeacher?.Invoke(teacher);
        }
    }
}
