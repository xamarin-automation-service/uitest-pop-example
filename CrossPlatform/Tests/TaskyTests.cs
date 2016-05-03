using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace CrossPlatform
{
    public class TaskyTests : AbstractSetup
    {
        public TaskyTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void EnterAndDeleteTask()
        {
            var taskName = "Finish example";

            EnterTask(taskName, "Cross-platform UITest");

            new TaskListPage()
                .SelectTask();

            new TaskDetailsPage()
                .Delete();

            new TaskListPage()
                .VerifyTaskDoesNotExist(taskName);
        }

        [Test]
        public void EnterAndCompleteTask()
        {
            var taskName = "Finish example";

            EnterTask(taskName, "Cross-platform UITest");

            new TaskListPage()
                .SelectTask();

            new TaskDetailsPage()
                .TapDone()
                .VerifyDone()
                .Save();

            new TaskListPage()
                .VerifyTaskDone(taskName, true);
        }


    }
}

