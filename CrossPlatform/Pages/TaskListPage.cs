using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace CrossPlatform
{
    public class TaskListPage : BasePage
    {
        readonly Query addTaskButton;
        readonly Query firstTask;
        readonly Query deleteButton;
        readonly Func<string, Query> checkMarkForTask;
        readonly Func<string, Query> taskListItem;

        protected override Trait Trait => new Trait
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
            app.WaitForElement(addTaskButton);
            app.Screenshot("Tapping add task button");
            app.Tap(addTaskButton);
        }

        public void SelectTask(string name = null)
        {
            app.WaitForElement(firstTask);

            if (name == null)
            {
                app.Screenshot("Selecting first task");
                app.Tap(firstTask);
            }
            else
            {
                app.Screenshot($"Selecting task: {name}");
                app.Tap(name);
            }
        }

        public TaskListPage VerifyTaskExists(string name)
        {
            app.WaitForElement(name);
            app.Screenshot($"Task exists: {name}");

            return this;
        }

        public TaskListPage VerifyTaskDoesNotExist(string name)
        {
            app.WaitForNoElement(name);
            app.Screenshot($"Task does not exist: {name}");

            return this;
        }

        public TaskListPage VerifyTaskDone(string name, bool done = true)
        {
            // Method not applicable to iOS
            if (OnAndroid)
            {
                if (done)
                {
                    app.WaitForElement(checkMarkForTask(name));
                    app.Screenshot("Task is done");
                }
                else
                {
                    app.WaitForNoElement(checkMarkForTask(name));
                    app.Screenshot("Task is not done");
                }
            }

            return this;
        }

        public TaskListPage DeleteTask(string name)
        {
            // Method not applicable to Android
            if (OniOS)
            {
                app.SwipeRightToLeft(taskListItem(name));
                app.Tap(deleteButton);

                app.Screenshot($"Task deleted: '{name}'");
            }

            return this;
        }
    }
}
