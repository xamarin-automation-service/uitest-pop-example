# Page object pattern sample

This project demonstrates the page object pattern (POP) that we use internally here at Xamarin. We have tried both simpler and more complex architectures in the past and have found this approach to be one of the best. It is easily adopted by people who are learning how to write tests, but also provides the scalability needed to build out larger, more complicated test suites.

This project is based on the [Tasky Pro Calabash example](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) and uses many of the same concepts as [Calabash's page object pattern](https://developer.xamarin.com/guides/testcloud/calabash/xplat-best-practices/).

## Want more information?

### [Read the wiki](https://github.com/xamarin-automation-service/uitest-pop-example/wiki)

## Running these tests on your computer

1. Clone this repo
1. Build
1. Run*

_*If you want to run this on a physical iOS device, you will need to clone and build the app from [source](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) in order to get an IPA file that is compatible with your device._

## POP Implementation

1. Create a new test project or open up your existing one.

1. Install the [Xamarin.UITest.POP NuGet package](https://www.nuget.org/packages/Xamarin.UITest.POP), which will add all the necessary files to your project. Alternatively, you can manually add [AppManager.cs](Xamarin.UITest.POPSample/AppManager.cs), [BaseTestFixture.cs](Xamarin.UITest.POPSample/BaseTestFixture.cs), [BasePage.cs](Xamarin.UITest.POPSample/BasePage.cs), and [Trait.cs](Xamarin.UITest.POPSample/Trait.cs) to your project (making sure to change the namespaces to match your own).

1. Create a folder for your test classes and a folder for your page classes. This will help keep things organized.

1. Create page object classes that inherit from `BasePage`. Make one for each page in your app.

1. Make sure that your test fixture classes inherit from `BaseTestFixture`.

1. From your tests, call methods on the page objects to automate your app.

## Contributors

* [Charles Wang](https://github.com/chawang)
* [Ethan Dennis](https://github.com/erdennis13)
* [Matisse Hack](https://github.com/MatisseHack)
* [Sweekriti Satpathy](https://github.com/Sweekriti91)
