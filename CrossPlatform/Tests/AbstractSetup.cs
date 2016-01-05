using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace CrossPlatform
{
    public class AbstractSetup
    {
        protected IApp app;
        protected Platform platform;

        public AbstractSetup(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        protected void EnterTask(string name, string notes = null)
        {
            new TaskListPage()
                .GoToAddTask();

            new TaskDetailsPage()
                .EnterTask(name, notes)
                .Save();

            new TaskListPage()
                .VerifyTaskExists(name);
        }
    }
}

