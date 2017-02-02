using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Xamarin.UITest.POPSample
{
    public class TaskDetailsPage : BasePage
    {
        readonly Query nameField;
        readonly Query notesField;
        readonly Query saveButton;
        readonly Query deleteButton;
        readonly Query doneIndicator;

        protected override PlatformQuery Trait
        {
            get
            {
                return new PlatformQuery
                {
                    Android = x => x.Marked("menu_save_task"),
                    iOS = x => x.Marked("Task Details")
                };
            }
        }

        public TaskDetailsPage()
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
                App.EnterText(nameField, name);

                if (notes != null)
                    App.EnterText(notesField, notes);
            }

            if (OniOS)
            {
                App.EnterText(nameField, name);
                App.PressEnter();

                if (notes != null)
                    App.EnterText(notesField, notes);

                App.PressEnter();
            }

            App.Screenshot($"Task \"{name}\" entered");

            return this;
        }

        public TaskDetailsPage TapDone()
        {
            App.DismissKeyboard();
            App.Tap(doneIndicator);
            App.Screenshot("Set Done");

            return this;
        }

        public TaskDetailsPage VerifyDone(bool done = true)
        {
            if (OnAndroid)
            {
                Assert.True((bool)App.Query(x => doneIndicator(x).Invoke("isChecked")).First());
                App.Screenshot("Task completed");
            }

            if (OniOS)
            {
                Assert.AreEqual(1, App.Query(x => doneIndicator(x).Invoke("isOn")).First());
                App.Screenshot("Task completed");
            }

            return this;
        }

        public void Save()
        {
            App.Screenshot("Tapping save");
            App.Tap(saveButton);
        }

        public void Delete()
        {
            App.Screenshot("Tapping delete");
            App.Tap(deleteButton);
        }
    }
}
