using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;

namespace MadStick.Tests.UAT
{

    public class BrowserUAT : IDisposable
    {

        private readonly ITestOutputHelper _output;
        protected IWebDriver driver;

        public BrowserUAT(ITestOutputHelper output)
        {
            _output = output;


            try
            {
                new DriverManager().SetUpDriver(new EdgeConfig());
            }
            catch (System.Exception e)
            {
                if (e is WebException)
                {
                    _output.WriteLine("No internet connection... , {0} cannot check and update binaries", nameof(DriverManager));

                }
                else
                {
                    throw;
                }
            }

            driver = new EdgeDriver();

            // set browser window to not hide menu items(in the mobile view)
            driver.Manage().Window.Size = new Size(1920, 1080);

        }

        public void Dispose()
        {
            driver.Close();
            driver.Dispose();
        }

        [Fact]
        public void EdgeSessionTest()
        {
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

            var title = driver.Title;
            Assert.Equal("Web form", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textBox = driver.FindElement(By.Name("my-text"));
            var submitButton = driver.FindElement(By.TagName("button"));

            textBox.SendKeys("Selenium");
            submitButton.Click();

            var message = driver.FindElement(By.Id("message"));
            var value = message.Text;
            Assert.Equal("Received!", value);
        }

        [Fact]
        public void ViewProducts()
        {
            driver.Navigate().GoToUrl("https://localhost:5001");

            var title = driver.Title;
            Assert.Equal("Home page - MadStickWebAppTester", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var productsLink = driver.FindElement(By.LinkText("Products"));

            productsLink.Click();

            // if the below line doesnt throw test is succcessful
            var products = driver.FindElements(By.CssSelector("table tbody tr"));
        }

        //registration
        [Fact]
        //requires to delete the registered user each time the test is run in order for the next same test not to fail
        public void RegisterWithValidData_CheckNewUserExists()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            string email = "temptestuser@test.test";
            string password = "Pa55swrd";

            IWebElement registerLink = driver.FindElement(By.LinkText("Register"));
            registerLink.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            IWebElement form = driver.FindElement(By.CssSelector("#registerForm"));

            IWebElement usernameInput = driver.FindElement(By.CssSelector("#registerForm input[type=\"email\"]"));
            usernameInput.SendKeys(email);

            ReadOnlyCollection<IWebElement> passwordInputs = driver.FindElements(By.CssSelector("#registerForm input[type=\"password\"]"));

            foreach (var p in passwordInputs)
            {
                p.SendKeys(password);
            }

            form.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.Equal("Register confirmation - MadStickWebAppTester", driver.Title);

            IWebElement confirmationLink = driver.FindElement(By.LinkText("Click here to confirm your account"));
            confirmationLink.Click();

            IWebElement homeLink = driver.FindElement(By.LinkText("Home"));
            homeLink.Click();

            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);

            IWebElement loginLink = driver.FindElement(By.LinkText("Login"));
            loginLink.Click();

            IWebElement loginForm = driver.FindElement(By.CssSelector("#account"));


            IWebElement emailToLogin = driver.FindElement(By.CssSelector("#account input[type=\"email\"]"));
            emailToLogin.SendKeys(email);

            IWebElement passwordToLogin = driver.FindElement(By.CssSelector("#account input[type=\"password\"]"));
            passwordToLogin.SendKeys(password);

            loginForm.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);

            string expectedText = "Hello " + email;

            IWebElement usernameLink = driver.FindElement(By.LinkText(expectedText));

            Assert.Contains(expectedText, usernameLink.Text);
        }

        [Fact]
        public void RegsiterWithInvalidData_CheckRegistrationFailed()
        {

            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            string email = "shouldBeValidEmail";
            string password = "passwordMustContainNonLetterCharacter";

            IWebElement registerLink = driver.FindElement(By.LinkText("Register"));
            registerLink.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            IWebElement form = driver.FindElement(By.CssSelector("#registerForm"));

            IWebElement usernameInput = driver.FindElement(By.CssSelector("#registerForm input[type=\"email\"]"));
            usernameInput.SendKeys(email);

            ReadOnlyCollection<IWebElement> passwordInputs = driver.FindElements(By.CssSelector("#registerForm input[type=\"password\"]"));

            foreach (var p in passwordInputs)
            {
                p.SendKeys(password);
            }

            form.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.NotEqual("Register confirmation - MadStickWebAppTester", driver.Title);
        }

        //sign in 
        [Fact]
        //requires existing user in db
        public void SignInWithValidData_CheckUserIsSelected()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            string email = "existingtestuser@tests.test";
            string password = "Pa55swrd";

            IWebElement loginLink = driver.FindElement(By.LinkText("Login"));
            loginLink.Click();

            IWebElement loginForm = driver.FindElement(By.CssSelector("#account"));

            IWebElement emailToLogin = driver.FindElement(By.CssSelector("#account input[type=\"email\"]"));
            emailToLogin.SendKeys(email);

            IWebElement passwordToLogin = driver.FindElement(By.CssSelector("#account input[type=\"password\"]"));
            passwordToLogin.SendKeys(password);

            loginForm.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);

            string expectedText = "Hello " + email;

            IWebElement usernameLink = driver.FindElement(By.LinkText(expectedText));

            Assert.Contains(expectedText, usernameLink.Text);
        }

        [Fact]
        public void SignInWithInvalidData_CheckUserIsNotSelected()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            string email = "doesntexistestuser@tests.test";
            string password = "Pa55swrd";

            IWebElement loginLink = driver.FindElement(By.LinkText("Login"));
            loginLink.Click();

            IWebElement loginForm = driver.FindElement(By.CssSelector("#account"));

            IWebElement emailToLogin = driver.FindElement(By.CssSelector("#account input[type=\"email\"]"));
            emailToLogin.SendKeys(email);

            IWebElement passwordToLogin = driver.FindElement(By.CssSelector("#account input[type=\"password\"]"));
            passwordToLogin.SendKeys(password);

            loginForm.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            Assert.Equal("Log in - MadStickWebAppTester", driver.Title);
        }

        //add to cart
        [Fact]
        //requires web app implementation
        public void AddToCartAProduct_ChecksIfProductWasAdded()
        {
            throw new NotImplementedException();
        }

        //filtering
        [Fact]
        //requires only 2 products with audi in their name
        public void FilterBySearchTerm_CheckOnlyItemsWithTheTerm()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            string searchTerm = "audi";

            IWebElement productsLink = driver.FindElement(By.LinkText("Products"));
            productsLink.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            Assert.Equal("Index - MadStickWebAppTester", driver.Title);

            IWebElement filterForm = driver.FindElement(By.CssSelector("#filterForm"));
            IWebElement searchBoxInput = driver.FindElement(By.CssSelector("#filterForm input[name=\"searchString\"]"));
            searchBoxInput.SendKeys(searchTerm);
            filterForm.Submit();

            ReadOnlyCollection<IWebElement> audis = driver.FindElements(By.CssSelector("tbody tr"));

            Assert.Equal(2, audis.Count);
        }

        [Fact]
        //requires web app implementation
        public void FilterBySearchTermAndPrice_CheckOnlyItemsWithTheTermAndPrice()
        {
            throw new NotImplementedException();
        }

        //contact us
        [Fact]
        //requires web app implementation
        public void SubmitForm_CheckFormSubmitted()
        {

            throw new NotImplementedException();
        }

        //deletion
        [Fact]
        //requires existing admin user in db
        public void DeleteProduct_CheckIfProductNotPresent()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl("https://localhost:5001");
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            string email = "ms@admin.com";
            string password = "Pa55swrd";

            IWebElement loginLink = driver.FindElement(By.LinkText("Login"));
            loginLink.Click();
            IWebElement loginForm = driver.FindElement(By.CssSelector("#account"));
            IWebElement emailToLogin = driver.FindElement(By.CssSelector("#account input[type=\"email\"]"));
            emailToLogin.SendKeys(email);
            IWebElement passwordToLogin = driver.FindElement(By.CssSelector("#account input[type=\"password\"]"));
            passwordToLogin.SendKeys(password);

            loginForm.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            Assert.Equal("Home page - MadStickWebAppTester", driver.Title);
            string expectedText = "Hello " + email;
            IWebElement usernameLink = driver.FindElement(By.LinkText(expectedText));
            Assert.Contains(expectedText, usernameLink.Text);






        }

        //buying
        [Fact]
        //requires web app  implementation
        public void Checkout_CheckOrderWasPlaced()
        {

            throw new NotImplementedException();
        }

    }
}