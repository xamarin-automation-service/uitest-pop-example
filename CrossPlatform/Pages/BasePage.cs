using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CrossPlatform
{
    public abstract class BasePage
    {
        protected readonly IApp app;
        protected readonly bool OnAndroid = false;
        protected readonly bool OniOS = false;

        protected BasePage(IApp app, Platform platform)
        {
            this.app = app;

            if (platform == Platform.Android)
                OnAndroid = true;
            else if (platform == Platform.iOS)
                OniOS = true;
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


