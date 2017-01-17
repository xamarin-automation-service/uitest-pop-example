using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace $rootnamespace$
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class AbstractSetup
    {
        protected IApp app;
        protected Platform platform;

        protected bool OnAndroid { get; set; }
        protected bool OniOS { get; set; }

        protected AbstractSetup(Platform platform)
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
    }
}