using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace CrossPlatform
{
    static class AppInitializer
    {
        const string apiKey = "YOUR_API_KEY";
        const string apkPath = "../../../TaskyDroid.apk";
        const string appPath = "../../../TaskyiOS.app";
        const string ipaBundleId = "com.xamarin.samples.taskytouch";

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile(apkPath)
                    .StartApp();
            }

            return ConfigureApp
                    .iOS
                    .AppBundle(appPath)
//                    .InstalledApp(ipaBundleId)
                    .StartApp();
        }
    }
}

