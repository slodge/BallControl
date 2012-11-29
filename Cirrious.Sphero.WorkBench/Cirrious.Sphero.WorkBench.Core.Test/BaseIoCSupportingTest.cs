// <copyright file="BaseIoCSupportingTest.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
//  
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com - Hire me - I'm worth it!

using Cirrious.MvvmCross.Console.Platform;
using Cirrious.MvvmCross.Core;
using Cirrious.MvvmCross.Interfaces.IoC;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.IoC;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Platform.Diagnostics;
using NUnit.Framework;

namespace Cirrious.Sphero.WorkBench.Core.Test
{
    public class BaseIoCSupportingTest
    {
        private IMvxIoCProvider _ioc;

        protected IMvxIoCProvider Ioc
        {
            get { return _ioc; }
        }

        [TestFixtureSetUp]
        public virtual void Setup()
        {
            // fake set up of the IoC
            MvxSingleton.ClearAllSingletons();
            _ioc = new MvxSimpleIoCServiceProvider();
            var serviceProvider = new MvxServiceProvider(_ioc);
            _ioc.RegisterServiceInstance<IMvxServiceProviderRegistry>(serviceProvider);
            _ioc.RegisterServiceInstance<IMvxServiceProvider>(serviceProvider);
            _ioc.RegisterServiceInstance<IMvxTrace>(new MvxDebugTrace());
            MvxTrace.Initialize();
        }
    }
}