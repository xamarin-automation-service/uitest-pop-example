# Info about this POP example solution

This project demonstrates the page object pattern (POP) that we use internally here at Xamarin. We have tried both simpler and more complex architectures in the past and have found this approach to be one of the best. It is easily adopted by people who are learning how to write tests, but also provides the scalability needed to build out larger, more complicated test suites.

This project is based on the [Tasky Pro Calabash example](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) and uses many of the same concepts as [Calabash's page object pattern](https://developer.xamarin.com/guides/testcloud/calabash/xplat-best-practices/).

## Running these tests on your computer

1. Clone this repo
1. Build
1. Run*

*_If you want to run this on a physical iOS device, you will need to clone and build the app from [source](https://developer.xamarin.com/samples/test-cloud/TaskyPro/TaskyPro-Calabash/) in order to get an IPA file that is compatible with your device._

# Implementing POP

Now that you have run this sample project using the Tasky sample app, here is some more information on how to set up a new project using the page object pattern and some guidelines on how best to implement the pattern.

## How to use this pattern in your own project

This guide will walk you through setting up your project for cross-platform tests using the page object pattern; with minor edits you can also use this pattern for single platform tests.

1. Grab this template for the [`BasePage`](https://gist.github.com/MatisseHack/001410e9c36074fbe820) class and add the file to your project. Make sure to edit the namespace to match your project.

1. Edit your [`AppInitializer`](CrossPlatform/AppInitializer.cs) class so that it saves a static instance of the initialized app before returning it from the `StartApp(platform)` method.

1. Add an abstract class called [`AbstractSetup`](CrossPlatform/AbstractSetup.cs) to your project for all your test classes to inherit from. Add the `[TestFixture(platform)]` attributes to that class as well as the `[Setup]` method that initializes the app.

1. Create a folder for your test classes and a folder for your page classes. This will help keep things organized.

1. Create a first page that inherits from `BasePage` and pass a trait into the base constructor.

1. Create a new test class that inherits from `AbstractSetup` and create a test method that calls your newly created page. Note: you will need to pass in a `Platform` object to the base class in the test classes.

You should now have the basic setup complete and can start implementing more pages and tests.

## Main rules

There are several rules that we follow to make our version of the page object pattern consistent and useful. We've written many projects with this pattern and here is what has worked for us.

### 1. Each page in the app is a class in your test code

* If you navigate away from a page or if a modal view covers the page so that you can no longer interact with the page underneath, then it's a new page and you should define a new class in your test code.

* The page defines a "trait" that it passes to the `BasePage`'s constructor. The trait is some element unique to that page when fully loaded. The page will wait for the trait to appear before you can execute any other methods.

### 2. Page classes save queries for all the elements on the page as fields at the top of the file

* Don't define any queries inside methods. Define them all as fields at the top of the class and assign them in the constructor based on the platform:
	
	```csharp
	public class LogInPage : BasePage
	{
		readonly Func<AppQuery, AppQuery> EmailField;
		readonly Func<AppQuery, AppQuery> PasswordField;
		readonly Func<AppQuery, AppQuery> LogInButton;

		public LogInPage()
			: base(x => x.Id("log-in-image"), x => x.Id("sign-in-image"))
		{
			// The same for both platforms
			LogInButton = x => x.Id("log-in-button");

			if (OnAndroid)
			{
				EmailField = x => x.Id("android-email-field");
				PasswordField = x => x.Id("android-password-field");
			}

			if (OniOS)
			{
				EmailField = x=>x.Id("iOS-email-field");
				PasswordField = x => x.Id("iOS-password-field");
			}
		}
	}
	```
	
* This makes it easy to reuse and change the queries as needed.

### 3. Page classes define methods for all the actions that can be taken on that page
	
* The method's interaction with the page in the app should be obvious based on the name so that tests are easily readable.

* If the method navigates away from the page (e.g. taps a button that goes to another page in the app), the method should return `void` since you can no longer call more methods on that page.

* Methods that do not navigate away from the page should return their own type (using `return this;`) so that more methods can be called on the same object. This creates a fluent interface that is easy to work with.

	```csharp
	new LogInPage()
		.EnterCredentials("name", "password")
		.ConfirmLogIn();
	```

	The benefit of the fluent interface is that you don't need to save the page to a variable, the code thus becomes more concise.

### 4. Test methods consist only of calls to page methods

* Don't use `app.` in `[Test]` methods, that is the pages' job

* With few exceptions, tests should only instantiate new pages and make calls on those pages:

	```csharp
	[Test]
	public void LogInAndExploreTest()
	{
		new LogInPage()
			.EnterCredentials("name", "password")
			.ConfirmLogIn();

		new HomePage()
			.SelectFirstItem();

		// ...
	}
	```

## Helpful tricks

* All the methods in UITest generally take in a query for the element to interact with. For example, `app.Tap(x => x.Id("log-in-button"))` uses the query `x => x.Id("log-in-button")` to define what to tap on. All queries are of type `Func<AppQuery, AppQuery>` and to define them you would need to write:

	```csharp
	Func<AppQuery, AppQuery> LogInButton;
	
	LogInButton = x => x.Id("log-in-button");
	```
	
	However, writing `Func<AppQuery, AppQuery>` for every query can get tiresome and isn't that descriptive. We can make defining queries easier by adding the following line with our using statements at the very top of the file:
	
	```csharp
	using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
	```
	
	This aliases `Func<AppQuery, AppQuery>` with `Query` so we can rewrite the first code like so:
	
	```csharp
	Query LogInButton;
	
	LogInButton = x => x.Id("log-in-button");
	```

* Remember to save queries for all the elements on the page as fields at the top of the file. But what if there are a lot of similar items on the page (e.g. list view items) and it doesn't make sense to define a new query for each one? You might want to pass a parameter into the query (e.g. the text or index of the item you are looking for) to make it easier.
	
	You can do this by defining a nested lambda expression that takes in the parameter that changes and _returns a query (lambda expression) based on that parameter_. This is how it would work:

	```csharp
	Func<string, Func<AppQuery, AppQuery>> ListItem;
	
	ListItem = itemName => x => x.Class("ListViewItem").Text(itemName);
	```
	
	Then to use it:
	
	```csharp
	app.Tap(ListItem("some item text"));
	```
	
	This is reusable and achieves our goal of keeping all query definitions at the top of our file, while still being functionally the same as writing:

	```csharp
	app.Tap(x => x.Class("ListViewItem").Text("some item text"));
	```

## General testing suggestions

### 1. Make each test self contained

Every test in your test suite should be able to run on its own without depending on any other tests to successfully run beforehand. Every test should start with a clean slate, as if the app was just installed on the phone.

For example, you should not have a single test that logs in to your app at the beginning of your suite and then have every subsequent test assume that the app is already logged in. If the log in test were to fail for any reason, every other test in the suite that relied on it would also fail. 

In order to save on valuable time, you can use [backdoor methods](https://developer.xamarin.com/guides/testcloud/uitest/working-with/backdoors/) to speed up repetitive setup tasks like logging in. You can also use backdoor methods to reset the app and put it in a particular "clean" state before each test starts.

### 2. Split tests up into small chunks wherever possible

Long tests are just as bad as interdependent tests. If the first part of a long test fails, then it won't finish the rest of the test.

For example, it's better to have one test to log in, one test to search for an item, and one test to purchase an item. This way, with three separate tests, even if there is a bug in the search feature you can still test the purchase feature.

[Backdoor methods](https://developer.xamarin.com/guides/testcloud/uitest/working-with/backdoors/) can, again, provide a fast way to skip some parts of a long process and put the app into a certain state. For example, you could use a backdoor to add a product to your shopping cart before running a check out test.

### 3. Be as deterministic as possible

* **Use backdoors to set up the app -** Backdoors can create a consistent app state so that every test starts with a clean slate.

* **Avoid branching on UI state -** Your test should expect a certain UI state and assert that it is as expected rather than querying the UI to determine the next step.
	
* **Avoid branching in general (`if`, `else` and `try`, `catch`) -** These are warning signs that you are not being as deterministic as you could be. They can be useful in certain situations, but be conscious about whether or not you can use an assertion instead.

* **Use assertions (`WaitForElement`, `WaitForNoElement`, and `Assert`) -** We write tests to confirm app behavior, assertions are essential to confirm conditions. Best practice would be to assert following every action.

* **Take many screenshots -** In Test Cloud, screenshots are the best way to see every step of a test's execution and visually identify bugs. Best practice would be to take a screenshot following every assertion (ie do some action, make assertion, take screenshot).
	
* **Try not to do anything randomly -** Test results should be repeatable between test runs, randomness inherently takes this away.
	
* **Try not to use `Thread.Sleep(int)` -** `WaitForElement` and related methods provide a much better alternative. The only exception to this rule is when you have an animation that needs to complete before you want to interact with any elements.
	
* **Be careful with loops, especially the `while` loop -** Make sure you don't have loops that will run forever if something goes wrong with your app. Limit them to a reasonable number of iterations.

# Contributors

* [Charles Wang](https://github.com/chawang)
* [Ethan Dennis](https://github.com/erdennis13)
* [Matisse Hack](https://github.com/MatisseHack)
* [Sweekriti Satpathy](https://github.com/Sweekriti91)
