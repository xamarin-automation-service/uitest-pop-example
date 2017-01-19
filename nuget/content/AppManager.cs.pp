using System;
using Xamarin.UITest;

namespace $rootnamespace$
{
    static class AppManager
    {
        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp(platform)' before trying to access it.");
                return app;
            }
        }

        static Platform? platform;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set. Call 'AppManager.StartApp(platform)' before trying to access it.");
                return platform.Value;
            }
        }

        public static IApp StartApp(Platform platform)
        {
            AppManager.platform = platform;

            if (platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    .StartApp();
            }
            else
            {
                app = ConfigureApp
                    .iOS
                    .StartApp();
            }

            return app;
        }
    }
}