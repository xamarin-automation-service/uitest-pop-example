using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace CrossPlatform
{
    public class TaskDetailsPage : BasePage
    {
        readonly Query nameField;
        readonly Query notesField;
        readonly Query saveButton;
        readonly Query deleteButton;
        readonly Query doneIndicator;

        public TaskDetailsPage()
            : base(x => x.Marked("menu_save_task"), x => x.Marked("Task Details"))
        {
            if (OnAndroid)
            {
                nameField = x => x.Marked("txtName");
                notesField = x => x.Marked("txtNotes");
                saveButton = x => x.Marked("menu_save_task");
                deleteButton = x => x.Marked("menu_delete_task");
                doneIndicator = x => x.Marked("chkDone");
            }

            if (OniOS)
            {
                nameField = x => x.Marked("task name");
                notesField = x => x.Marked("other task info");
                saveButton = x => x.Marked("Save");
                deleteButton = x => x.Marked("Delete");
                doneIndicator = x => x.Class("UISwitch");
            }
        }

        public TaskDetailsPage EnterTask(string name, string notes = null)
        {
            if (OnAndroid)
            {
                app.EnterText(nameField, name);

                if (notes != null)
                    app.EnterText(notesField, notes);
            }

            if (OniOS)
            {
                app.EnterText(nameField, name);
                app.PressEnter();

                if (notes != null)
                    app.EnterText(notesField, notes);

                app.PressEnter();
            }

            app.Screenshot($"Task \"{name}\" entered");

            return this;
        }

        public TaskDetailsPage TapDone()
        {
            app.DismissKeyboard();
            app.Tap(doneIndicator);
            app.Screenshot("Set Done");

            return this;
        }

        public TaskDetailsPage VerifyDone(bool done = true)
        {
            if (OnAndroid)
            {
                Assert.True((bool)app.Query(x => doneIndicator(x).Invoke("isChecked")).First());
                app.Screenshot("Task completed");
            }

            if (OniOS)
            {
                Assert.AreEqual(1, (int)app.Query(x => doneIndicator(x).Invoke("isOn")).First());
                app.Screenshot("Task completed");
            }

            return this;
        }

        public void Save()
        {
            app.Screenshot("Tapping save");
            app.Tap(saveButton);
        }

        public void Delete()
        {
            app.Screenshot("Tapping delete");
            app.Tap(deleteButton);
        }
    }
}
