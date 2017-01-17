To use:
1. AppInitializer logic is now replaced by AppManager. You can safely remove AppInitializer after you move any user-added code to AppManager.cs 
2. Ensure page objects inherit from BasePage
3. Ensure test classes inherit from BaseTestFixture

For more information regarding this pattern, please visit the project site:
https://github.com/xamarin-automation-service/uitest-pop-example

Contributors:
- Charles Wang
- Ethan Dennis
- Matisse Hack
- Sweekriti Satpathy