using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.UITest.POPSample
{
    public abstract class BasePage
    {
        protected IApp App { get; private set; }
        protected bool OnAndroid { get; private set; }
        protected bool OniOS { get; private set; }

        protected abstract Trait Trait { get; }

        protected BasePage()
        {
            App = AppManager.App;
            OnAndroid = AppManager.Platform == Platform.Android;
            OniOS = AppManager.Platform == Platform.iOS;

            InitializeCommonQueries();

            AssertOnPage(TimeSpan.FromSeconds(30));

            App.Screenshot("On " + this.GetType().Name);
        }

        /// <summary>
        /// Verifies that the trait is still present. Defaults to no wait.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        protected void AssertOnPage(TimeSpan? timeout = default(TimeSpan?))
        {
            var message = "Unable to verify on page: " + this.GetType().Name;

            if (timeout == null)
                Assert.IsNotEmpty(App.Query(Trait.Current), message);
            else
                Assert.DoesNotThrow(() => App.WaitForElement(Trait.Current, timeout: timeout), message);
        }

        /// <summary>
        /// Verifies that the trait is no longer present. Defaults to a two second wait.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        protected void WaitForPageToLeave(TimeSpan? timeout = default(TimeSpan?))
        {
            timeout = timeout ?? TimeSpan.FromSeconds(2);
            var message = "Unable to verify *not* on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => App.WaitForNoElement(Trait.Current, timeout: timeout), message);
        }

        #region CommonPageActions

        // Use this region to define functionality that is common across many or all pages in your app.
        // Eg tapping the back button of a page or selecting the tabs of a tab bar

        void InitializeCommonQueries()
        {
            if (OnAndroid)
            {
            }

            if (OniOS)
            {
            }
        }

        #endregion
    }
}
