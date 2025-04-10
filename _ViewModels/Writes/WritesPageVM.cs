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
using TaskAppT2._Views.Writes;

namespace TaskAppT2._ViewModels.Writes
{
    partial class WritesPageVM : ObservableObject
    {
        public WritesPageVM(LocalDataBase db) 
        {
            this.db = db;
            db.SavedChanges += GetData;
            WritePage.OnNewFindText += UpdateFindText;
            GetData(null, null);
        }

        ~WritesPageVM()
        {
            db.SavedChanges -= GetData;
            WritePage.OnNewFindText -= UpdateFindText;
        }

        [ObservableProperty]
        public partial ObservableCollection<Note> Notes { get; set; } = null!;

        [ObservableProperty]
        public partial string? NoteName { get; set; }

        [ObservableProperty]
        public partial string? NoteText { get; set; }

        readonly LocalDataBase db;
        List<Note> allNotes = null!;
        AddNewNoteModalPage? modalPage;
        Note? editNote;

        private void GetData(object? sender, SavedChangesEventArgs? e)
        {
            allNotes = [.. db.Notes];
            Notes = allNotes.ToObservableCollection();
        }

        private void UpdateFindText(string str)
        {
            Notes = allNotes.Where(n => n.Name.StartsWith(str, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection();
        }

        [RelayCommand]
        void AddNewNote()
        {
            modalPage ??= new AddNewNoteModalPage();
            NoteName = null;
            NoteText = null;
            Shell.Current.Navigation.PushModalAsync(modalPage);
        }

        [RelayCommand]
        async Task SavedChanges()
       {
            if (NoteName != null && NoteName != string.Empty)
            {
                int count = allNotes.Count(n => n.Name.ToLower() == NoteName.ToLower());
                if (editNote == null)
                {
                    if (count != 0)
                    {
                        PopUI.ShowSnackErr("Уже есть заметка с таким заголовком!");
                        return;
                    }
                    await db.Notes.AddAsync(new Note(NoteName) { Text = NoteText });
                    await db.SaveChangesAsync();
                }
                else
                {
                    if (count > 1)
                    {
                        PopUI.ShowSnackErr("Уже есть заметка с таким заголовком!");
                        return;
                    }
                    editNote.Name = NoteName;
                    editNote.Text = NoteText;
                    db.Notes.Update(editNote);
                    await db.SaveChangesAsync();
                }
                await Shell.Current.Navigation.PopModalAsync();
            }
            else
            {
                PopUI.ShowSnackErr("Задайте заголовок!");
                return;
            }
        }

        [RelayCommand]
        void EditNote(Note note)
        {
            modalPage ??= new AddNewNoteModalPage();
            NoteName = note.Name;
            NoteText = note.Text;
            editNote = note;
            Shell.Current.Navigation.PushModalAsync(modalPage);
        }

        [RelayCommand]
        async Task DeleteNote()
        {
            if(editNote != null)
            {
                db.Notes.Remove(editNote);
                await Shell.Current.Navigation.PopModalAsync();
                await db.SaveChangesAsync();
                editNote = null;
            }
        }
    }
}
