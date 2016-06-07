using System;
using NUnit.Framework;
using Xamarin.UITest;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace CrossPlatform
{
    public class TaskListPage : BasePage
    {
        readonly Query AddTaskButton;
        readonly Query FirstTask;
        readonly Func<string, Query> CheckMarkForTask;
        readonly Func<string, Query> TaskListItem;
        readonly Query DeleteButton;

        public TaskListPage()
            : base(x => x.Marked("menu_add_task"), x => x.Marked("Tasky"))
        {
            if (OnAndroid)
            {
                AddTaskButton = x => x.Marked("menu_add_task");
                FirstTask = x => x.Id("lstTasks").Child(0);
                CheckMarkForTask = taskName => x => x.Marked(taskName).Parent().Sibling().Id("checkMark");
            }

            if (OniOS)
            {
                AddTaskButton = x => x.Marked("Add");
                FirstTask = x => x.Class("UITableViewWrapperView").Child(0);
                TaskListItem = name => x => x.Class("UITableViewCell").Text(name);
                DeleteButton = x => x.Marked("Delete");
            }
        }

        public void GoToAddTask()
        {
            app.Repl();
            app.WaitForElement(AddTaskButton);
            app.Screenshot("Tapping add task button");
            app.Tap(AddTaskButton);
        }

        public void SelectTask(string name = null)
        {
            app.WaitForElement(FirstTask);

            if (name == null)
            {
                app.Screenshot("Selecting first task");
                app.Tap(FirstTask);
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
                    app.WaitForElement(CheckMarkForTask(name));
                    app.Screenshot("Task is done");
                }
                else
                {
                    app.WaitForNoElement(CheckMarkForTask(name));
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
                app.SwipeRightToLeft(TaskListItem(name));
                app.Tap(DeleteButton);

                app.Screenshot($"Task deleted: '{name}'");
            }

            return this;
        }
    }
}

