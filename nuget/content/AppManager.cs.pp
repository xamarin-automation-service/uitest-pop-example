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

        public static void StartApp(Platform platform)
        {
            AppManager.platform = platform;

            if (platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    // Used to run a .apk file:
                    //.ApkFile("path/to/file.apk")
                    .StartApp();
            }

            if (platform == Platform.iOS)
            {
                app = ConfigureApp
                    .iOS
                    // Used to run a .app file on an ios simulator:
                    //.AppBundle("path/to/file.app")
                    // Used to run a .ipa file on a physical ios device:
                    //.InstalledApp("com.company.bundleid")
                    .StartApp();
            }
        }
    }
}