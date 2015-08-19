using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace CrossPlatform
{
    public class TaskDetailsPage
    {
        readonly IApp app;
        readonly Platform platform;

        readonly string NameField;
        readonly string NotesField;
        readonly string SaveButton;
        readonly string DeleteButton;

        // Query aliases Func<AppQuery, AppQuery> via the above using statement
        readonly Query DoneCheck;

        public TaskDetailsPage(IApp app, Platform platform)
        {
            this.app = app;
            this.platform = platform;

            string pageTrait = null;

            if (platform == Platform.Android)
            {
                NameField = "txtName";
                NotesField = "txtNotes";
                SaveButton = "menu_save_task";
                DeleteButton = "menu_delete_task";
                DoneCheck = e => e.Marked("chkDone");

                pageTrait = SaveButton;
            }

            if (platform == Platform.iOS)
            {
                NameField = "task name";
                NotesField = "other task info";
                SaveButton = "Save";
                DeleteButton = "Delete";
                DoneCheck = e => e.Class("UISwitch");

                pageTrait = "Task Details";
            }

            app.WaitForElement(pageTrait);
            app.Screenshot("On task details page");
        }

        public TaskDetailsPage EnterTask(string name, string notes = null)
        {
            if (platform == Platform.Android)
            {
                app.EnterText(NameField, name);
                app.Back();
                if (notes != null)
                {
                    app.EnterText(NotesField, notes);
                    app.Back();
                }
            }

            if (platform == Platform.iOS)
            {
                app.EnterText(NameField, name);
                app.PressEnter();
                if (notes != null)
                {
                    app.EnterText(NotesField, notes);
                }
                app.PressEnter();
            }

            app.Screenshot($"Task \"{name}\" entered");

            return this;
        }

        public TaskDetailsPage TapDone()
        {
            app.DismissKeyboard();
            app.Tap(DoneCheck);
            app.Screenshot("Set Done");

            return this;
        }

        public TaskDetailsPage VerifyDone(bool done = true)
        {
            if (platform == Platform.Android)
            {
                Assert.True((bool)app.Query(x => x.Marked("chkDone").Invoke("isChecked")).First());
                app.Screenshot("Task completed");
            }

            if (platform == Platform.iOS)
            {
                Assert.AreEqual(1, app.Query(x => x.Class("UISwitch").Invoke("isOn")).First());
                app.Screenshot("Task completed");
            }

            return this;
        }

        public void Save()
        {
            app.Tap(SaveButton);
        }

        public void Delete()
        {
            app.Tap(DeleteButton);
        }
    }
}

