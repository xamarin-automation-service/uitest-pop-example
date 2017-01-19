using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace $rootnamespace$
{
    public class Trait
    {
        public Func<AppQuery, AppQuery> Android { private get; set; }
        public Func<AppQuery, AppQuery> iOS { private get; set; }

        public Func<AppQuery, AppQuery> Current
        {
            get
            {
                switch (AppManager.Platform)
                {
                    case Platform.Android:
                        return Android;
                    case Platform.iOS:
                        return iOS;
                    default:
                        throw new PlatformNotSupportedException();
                }
            }
        }
    }
}