Xamarin.UITest.POP has created the following files:
- AppManager.cs
- BasePage.cs
- BaseTestFixture.cs
- PlatformQuery.cs

To use:
- Remove AppInitializer.cs (if it exists)
- Ensure page object classes inherit from BasePage
- Ensure test fixture classes inherit from BaseTestFixture. Your test fixtures should look like this:

public class Tests : BaseTestFixture
{
    public Tests(Platform platform)
        : base(platform)
    {
    }

    [Test]
    public void AppLaunches()
    {
        app.Screenshot("First screen.");
    }
}

For more information regarding the page object pattern, please visit the project site:
https://github.com/xamarin-automation-service/uitest-pop-example

Contributors:
- Charles Wang
- Ethan Dennis
- Matisse Hack
- Sweekriti Satpathy