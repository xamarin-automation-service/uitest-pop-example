using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Xamarin.UITest.POPSample
{
    public class TaskyTests : BaseTestFixture
    {
        public TaskyTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void Repl()
        {
            if (TestEnvironment.IsTestCloud)
                Assert.Ignore("Local only");

            App.Repl();
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
