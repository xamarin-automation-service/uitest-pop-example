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
                if (AppManager.Platform == Platform.Android && Android != null)
                {
                    return Android;
                }

                if (AppManager.Platform == Platform.iOS && iOS != null)
                {
                    return iOS;
                }

                throw new NullReferenceException("Trait not set for current platform");
            }
        }
    }
}