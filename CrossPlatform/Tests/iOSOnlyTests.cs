using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace CrossPlatform
{
    [TestFixture(Platform.iOS)]
    public class iOSOnlyTests : AbstractSetup
    {
        public iOSOnlyTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void AddAndDeleteTaskFromList()
        {
            var taskName = "Finish example";

            EnterTask(taskName, "Cross-platform UITest");

            new TaskListPage()
                .DeleteTask(taskName)
                .VerifyTaskDoesNotExist(taskName);
        }
    }
}

