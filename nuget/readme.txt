Xamarin.UITest.POP has created the following files (you can now remove this NuGet package if you wish):
- AppManager.cs
- BasePage.cs
- BaseTestFixture.cs
- Trait.cs

To use:
- Remove AppInitializer.cs (if it exists)
- Ensure page object classes inherit from BasePage
- Ensure test fixture classes inherit from BaseTestFixture

For more information regarding the page object pattern, please visit the project site:
https://github.com/xamarin-automation-service/uitest-pop-example

Contributors:
- Charles Wang
- Ethan Dennis
- Matisse Hack
- Sweekriti Satpathy