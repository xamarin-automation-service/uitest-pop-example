using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace CrossPlatform
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
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

            new TaskListPage(app, platform)
                .SelectTask();

            new TaskDetailsPage(app, platform)
                .Delete();

            new TaskListPage(app, platform)
                .VerifyTaskDoesNotExist(taskName);
        }

        [Test]
        public void EnterAndCompleteTask()
        {
            var taskName = "Finish example";

            EnterTask(taskName, "Cross-platform UITest");

            new TaskListPage(app, platform)
                .SelectTask();

            new TaskDetailsPage(app, platform)
                .TapDone()
                .VerifyDone()
                .Save();

            new TaskListPage(app, platform)
                .VerifyTaskDone(taskName, true);
        }


    }
}

