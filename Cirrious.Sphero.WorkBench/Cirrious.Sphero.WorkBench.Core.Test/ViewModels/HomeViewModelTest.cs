// <copyright file="HomeViewModelTest.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System.Linq;
using Cirrious.MvvmCross.Interfaces.Views;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Test.Mocks;
using Cirrious.Sphero.WorkBench.Core.ViewModels;
using Moq;
using NUnit.Framework;

namespace Cirrious.Sphero.WorkBench.Core.Test.ViewModels
{
    [TestFixture]
    public class HomeViewModelTest : BaseIoCSupportingTest
    {
        [TestCase]
        public void RefreshListCommandCallsList()
        {
            var mockNavigation = new MockMvxViewDispatcher();
            var mockNavigationProvider = new MockMvxViewDispatcherProvider();
            mockNavigationProvider.Dispatcher = mockNavigation;
            Ioc.RegisterServiceInstance<IMvxViewDispatcherProvider>(mockNavigationProvider);

            var spheroListService = new Mock<ISpheroListService>();
            Ioc.RegisterServiceInstance(spheroListService.Object);

            var viewModel = new HomeViewModel();
            viewModel.RefreshListCommand.Execute(null);

            spheroListService.Verify(service => service.RefreshList(), Times.Once());
        }

        [TestCase]
        public void GoToSpheroCommandCallsNavigate()
        {
            var mockNavigation = new MockMvxViewDispatcher();
            var mockNavigationProvider = new MockMvxViewDispatcherProvider();
            mockNavigationProvider.Dispatcher = mockNavigation;
            Ioc.RegisterServiceInstance<IMvxViewDispatcherProvider>(mockNavigationProvider);

            var spheroListService = new Mock<ISpheroListService>();
            Ioc.RegisterServiceInstance(spheroListService.Object);

            var mockAvailableSphero = new Mock<IAvailableSphero>();
            mockAvailableSphero.SetupGet(s => s.Name).Returns("TestSphero");

            var viewModel = new HomeViewModel();
            viewModel.GoToSpheroCommand.Execute(mockAvailableSphero.Object);

            Assert.That(mockNavigation.NavigateRequests.Count == 1);
            Assert.That(mockNavigation.NavigateRequests.First().ViewModelType == typeof (SpheroViewModel));
            Assert.That(mockNavigation.NavigateRequests.First().ParameterValues.Count == 1);
            Assert.That(mockNavigation.NavigateRequests.First().ParameterValues["name"] == "TestSphero");
        }
    }
}