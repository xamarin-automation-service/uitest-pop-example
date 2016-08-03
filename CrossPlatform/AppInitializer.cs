using System;
using Xamarin.UITest;

namespace CrossPlatform
{
    static class AppInitializer
    {
        const string ApkPath = "../../../Binaries/TaskyDroid.apk";
        const string AppPath = "../../../Binaries/TaskyiOS.app";
        const string IpaBundleId = "com.xamarin.samples.taskytouch";

        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppInitializer.App' not set. Call 'AppInitializer.StartApp(platform)' before trying to access it.");
                return app;
            }
        }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    .ApkFile(ApkPath)
                    .StartApp();
            }
            else
            {
                app = ConfigureApp
                    .iOS
                    .AppBundle(AppPath)
                    // This is used to run an ipa on a physical ios device
                    //.InstalledApp(ipaBundleId)
                    .StartApp();
            }

            return app;
        }
    }
}
