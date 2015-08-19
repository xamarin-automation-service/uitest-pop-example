using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CrossPlatform
{
    public class BasePage
    {
        protected readonly IApp app;
        protected readonly bool OnAndroid;
        protected readonly bool OniOS;

        protected BasePage(IApp app, Platform platform)
        {
            this.app = app;

            OnAndroid = platform == Platform.Android;
            OniOS = platform == Platform.iOS;
        }

        protected BasePage(IApp app, Platform platform, Func<AppQuery, AppQuery> androidTrait, Func<AppQuery, AppQuery> iOSTrait)
            : this(app, platform)
        {
            if (OnAndroid)
                app.WaitForElement(androidTrait);
            if (OniOS)
                app.WaitForElement(iOSTrait);

            app.Screenshot("On " + this.GetType().Name);
        }

        protected BasePage(IApp app, Platform platform, string androidTrait, string iOSTrait)
            : this(app, platform, x => x.Marked(androidTrait), x => x.Marked(iOSTrait))
        {
        }
    }
}


