// <copyright file="SpheroListServiceTest.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sphero.Interfaces;
using Cirrious.Sphero.WorkBench.Core.Services;
using Moq;
using NUnit.Framework;

namespace Cirrious.Sphero.WorkBench.Core.Test.Services
{
    [TestFixture]
    public class SpheroListServiceTest
        : BaseIoCSupportingTest
    {
        [TestCase]
        public void ConstructionCallsRefreshList()
        {
            var mockFinder = new Mock<ISpheroFinder>();
            Ioc.RegisterServiceInstance(mockFinder.Object);

            var service = new SpheroListService();
            Assert.IsTrue(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);

            mockFinder.Verify(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()),
                Times.Once());
        }

        [TestCase]
        public void CallingRefreshMultipleTimesConcurrentlyJustResultsInOneCall()
        {
            var mockFinder = new Mock<ISpheroFinder>();
            Ioc.RegisterServiceInstance(mockFinder.Object);

            var service = new SpheroListService();
            Assert.IsTrue(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);

            service.RefreshList();
            service.RefreshList();
            service.RefreshList();
            service.RefreshList();
            service.RefreshList();

            mockFinder.Verify(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()),
                Times.Once());
        }

        [TestCase]
        public void CallingRefreshAfterCausesNewRefresh()
        {
            var mockFinder = new Mock<ISpheroFinder>();
            Ioc.RegisterServiceInstance(mockFinder.Object);

            Action<IList<IAvailableSphero>> successCallback = null;
            Action<Exception> failureCallback = null;
            mockFinder.Setup(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()))
                      .Callback((Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError) =>
                          {
                              successCallback = onSuccess;
                              failureCallback = onError;
                          });

            var service = new SpheroListService();
            Assert.IsTrue(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);

            var mockList = new List<IAvailableSphero>();
            successCallback(mockList);

            Assert.IsFalse(service.IsRefreshing);
            service.RefreshList();

            Assert.IsTrue(service.IsRefreshing);
            mockFinder.Verify(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()),
                Times.Exactly(2));
        }

        [TestCase]
        public void ErrorCallbackSetsListAndClearsIsRefreshing()
        {
            var mockFinder = new Mock<ISpheroFinder>();
            Ioc.RegisterServiceInstance(mockFinder.Object);
            Action<IList<IAvailableSphero>> successCallback = null;
            Action<Exception> failureCallback = null;
            mockFinder.Setup(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()))
                      .Callback((Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError) =>
                          {
                              successCallback = onSuccess;
                              failureCallback = onError;
                          });

            var service = new SpheroListService();
            Assert.IsTrue(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);

            failureCallback(new Exception());

            Assert.IsFalse(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);
        }

        [TestCase]
        public void SuccessCallbackSetsListAndClearsIsRefreshing()
        {
            var mockFinder = new Mock<ISpheroFinder>();
            Ioc.RegisterServiceInstance(mockFinder.Object);
            Action<IList<IAvailableSphero>> successCallback = null;
            Action<Exception> failureCallback = null;
            mockFinder.Setup(
                finder => finder.Find(It.IsAny<Action<IList<IAvailableSphero>>>(), It.IsAny<Action<Exception>>()))
                      .Callback((Action<IList<IAvailableSphero>> onSuccess, Action<Exception> onError) =>
                          {
                              successCallback = onSuccess;
                              failureCallback = onError;
                          });

            var service = new SpheroListService();
            Assert.IsTrue(service.IsRefreshing);
            Assert.IsNull(service.AvailableSpheros);

            var mockList = new List<IAvailableSphero>();
            successCallback(mockList);

            Assert.IsFalse(service.IsRefreshing);
            Assert.IsNotNull(service.AvailableSpheros);
            Assert.AreSame(mockList, service.AvailableSpheros);
        }
    }
}