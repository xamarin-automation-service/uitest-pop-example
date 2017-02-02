using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Xamarin.UITest.POPSample
{
    public class TaskListPage : BasePage
    {
        readonly Query addTaskButton;
        readonly Query firstTask;
        readonly Query deleteButton;
        readonly Func<string, Query> checkMarkForTask;
        readonly Func<string, Query> taskListItem;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("menu_add_task"),
            iOS = x => x.Marked("Tasky")
        };

        public TaskListPage()
        {
            if (OnAndroid)
            {
                addTaskButton = x => x.Marked("menu_add_task");
                firstTask = x => x.Id("lstTasks").Child(0);
                checkMarkForTask = taskName => x => x.Marked(taskName).Parent().Sibling().Id("checkMark");
            }

            if (OniOS)
            {
                addTaskButton = x => x.Marked("Add");
                firstTask = x => x.Class("UITableViewWrapperView").Child(0);
                taskListItem = name => x => x.Class("UITableViewCell").Text(name);
                deleteButton = x => x.Marked("Delete");
            }
        }

        public void GoToAddTask()
        {
            App.WaitForElement(addTaskButton);
            App.Screenshot("Tapping add task button");
            App.Tap(addTaskButton);
        }

        public void SelectTask(string name = null)
        {
            App.WaitForElement(firstTask);

            if (name == null)
            {
                App.Screenshot("Selecting first task");
                App.Tap(firstTask);
            }
            else
            {
                App.Screenshot($"Selecting task: {name}");
                App.Tap(name);
            }
        }

        public TaskListPage VerifyTaskExists(string name)
        {
            App.WaitForElement(name);
            App.Screenshot($"Task exists: {name}");

            return this;
        }

        public TaskListPage VerifyTaskDoesNotExist(string name)
        {
            App.WaitForNoElement(name);
            App.Screenshot($"Task does not exist: {name}");

            return this;
        }

        public TaskListPage VerifyTaskDone(string name, bool done = true)
        {
            // Method not applicable to iOS
            if (OnAndroid)
            {
                if (done)
                {
                    App.WaitForElement(checkMarkForTask(name));
                    App.Screenshot("Task is done");
                }
                else
                {
                    App.WaitForNoElement(checkMarkForTask(name));
                    App.Screenshot("Task is not done");
                }
            }

            return this;
        }

        public TaskListPage DeleteTask(string name)
        {
            // Method not applicable to Android
            if (OniOS)
            {
                App.SwipeRightToLeft(taskListItem(name));
                App.Tap(deleteButton);

                App.Screenshot($"Task deleted: '{name}'");
            }

            return this;
        }
    }
}
