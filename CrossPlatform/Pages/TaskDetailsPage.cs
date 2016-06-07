using System;
using NUnit.Framework;
using Xamarin.UITest;
using System.Linq;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace CrossPlatform
{
    public class TaskDetailsPage : BasePage
    {
        readonly Query NameField;
        readonly Query NotesField;
        readonly Query SaveButton;
        readonly Query DeleteButton;
        readonly Query DoneIndicator;

        public TaskDetailsPage()
            : base(x => x.Marked("menu_save_task"), x => x.Marked("Task Details"))
        {
            if (OnAndroid)
            {
                NameField = x => x.Marked("txtName");
                NotesField = x => x.Marked("txtNotes");
                SaveButton = x => x.Marked("menu_save_task");
                DeleteButton = x => x.Marked("menu_delete_task");
                DoneIndicator = x => x.Marked("chkDone");
            }

            if (OniOS)
            {
                NameField = x => x.Marked("task name");
                NotesField = x => x.Marked("other task info");
                SaveButton = x => x.Marked("Save");
                DeleteButton = x => x.Marked("Delete");
                DoneIndicator = x => x.Class("UISwitch");
            }
        }

        public TaskDetailsPage EnterTask(string name, string notes = null)
        {
            if (OnAndroid)
            {
                app.EnterText(NameField, name);

                if (notes != null)
                    app.EnterText(NotesField, notes);
            }

            if (OniOS)
            {
                app.EnterText(NameField, name);
                app.PressEnter();

                if (notes != null)
                    app.EnterText(NotesField, notes);

                app.PressEnter();
            }

            app.Screenshot($"Task \"{name}\" entered");

            return this;
        }

        public TaskDetailsPage TapDone()
        {
            app.DismissKeyboard();
            app.Tap(DoneIndicator);
            app.Screenshot("Set Done");

            return this;
        }

        public TaskDetailsPage VerifyDone(bool done = true)
        {
            if (OnAndroid)
            {
                Assert.True((bool)app.Query(x => DoneIndicator(x).Invoke("isChecked")).First());
                app.Screenshot("Task completed");
            }

            if (OniOS)
            {
                Assert.AreEqual(1, (int)app.Query(x => DoneIndicator(x).Invoke("isOn")).First());
                app.Screenshot("Task completed");
            }

            return this;
        }

        public void Save()
        {
            app.Screenshot("Tapping save");
            app.Tap(SaveButton);
        }

        public void Delete()
        {
            app.Screenshot("Tapping delete");
            app.Tap(DeleteButton);
        }
    }
}

