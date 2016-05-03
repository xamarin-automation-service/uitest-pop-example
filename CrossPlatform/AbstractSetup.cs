using System;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace CrossPlatform
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class AbstractSetup
    {
        protected IApp app;
        protected Platform platform;
        protected bool OnAndroid;
        protected bool OniOS;

        public AbstractSetup(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            OnAndroid = app.GetType() == typeof(AndroidApp);
            OniOS = app.GetType() == typeof(iOSApp);
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

