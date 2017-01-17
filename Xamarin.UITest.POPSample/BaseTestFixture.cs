using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Xamarin.UITest.POPSample
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class BaseTestFixture
    {
        protected IApp app;
        protected Platform platform;

        protected bool OnAndroid { get; set; }
        protected bool OniOS { get; set; }

        protected BaseTestFixture(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            app = AppManager.StartApp(platform);

            OnAndroid = AppManager.Platform == Platform.Android;
            OniOS = AppManager.Platform == Platform.iOS;
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
