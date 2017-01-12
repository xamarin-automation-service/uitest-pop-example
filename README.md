## Info about this POP example solution

This project demonstrates the page object pattern (POP) that we use internally here at Xamarin. We have tried both simpler and more complex architectures in the past and have found this approach to be one of the best. It is easily adopted by people who are learning how to write tests, but also provides the scalability needed to build out larger, more complicated test suites.

This project is based on the [Tasky Pro Calabash example](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) and uses many of the same concepts as [Calabash's page object pattern](https://developer.xamarin.com/guides/testcloud/calabash/xplat-best-practices/).

## Want more information?

### [Read the wiki](https://github.com/xamarin-automation-service/uitest-pop-example/wiki)

## Running these tests on your computer

1. Clone this repo
1. Build
1. Run*

*_If you want to run this on a physical iOS device, you will need to clone and build the app from [source](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) in order to get an IPA file that is compatible with your device._

## POP Implementation

This guide will walk you through setting up your project for cross-platform tests using the page object pattern; with minor edits you can also use this pattern for single platform tests.

1. Grab this template for the [`BasePage`](https://gist.github.com/MatisseHack/001410e9c36074fbe820) class and add the file to your project. Make sure to edit the namespace to match your project.

1. Edit your [`AppInitializer`](CrossPlatform/AppInitializer.cs) class so that it saves a static instance of the initialized app before returning it from the `StartApp(platform)` method.

1. Add an abstract class called [`AbstractSetup`](CrossPlatform/AbstractSetup.cs) to your project for all your test classes to inherit from. Add the `[TestFixture(platform)]` attributes to that class as well as the `[Setup]` method that initializes the app.

1. Create a folder for your test classes and a folder for your page classes. This will help keep things organized.

1. Create a first page that inherits from `BasePage` and pass a trait into the base constructor.

1. Create a new test class that inherits from `AbstractSetup` and create a test method that calls your newly created page. Note: you will need to pass in a `Platform` object to the base class in the test classes.

You should now have the basic setup complete and can start implementing more pages and tests.

## Contributors

* [Charles Wang](https://github.com/chawang)
* [Ethan Dennis](https://github.com/erdennis13)
* [Matisse Hack](https://github.com/MatisseHack)
* [Sweekriti Satpathy](https://github.com/Sweekriti91)
