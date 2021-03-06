﻿// MvxChildViewModelOwnerAdapter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Cirrious.CrossCore.Droid.Views;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.Droid.Fragging.EventSource;
using Cirrious.MvvmCross.Droid.Views;

namespace Cirrious.MvvmCross.Droid.Fragging
{
    public class MvxChildViewModelOwnerAdapterFragmentActivity : MvxBaseFragmentActivityAdapter
    {
        protected IMvxChildViewModelOwner ChildOwner
        {
            get { return (IMvxChildViewModelOwner) base.Activity; }
        }

        public MvxChildViewModelOwnerAdapterFragmentActivity(IMvxEventSourceFragmentActivity eventSource)
            : base(eventSource)
        {
            if (!(eventSource is IMvxChildViewModelOwner))
            {
                throw new MvxException("You cannot use a MvxChildViewModelOwnerAdapterFA on {0}",
                    eventSource.GetType().Name);
            }
        }           

        protected override void EventSourceOnDestroyCalled(object sender, EventArgs eventArgs)
        {
            ChildOwner.ClearOwnedSubIndicies();
            base.EventSourceOnDestroyCalled(sender, eventArgs);
        }

        protected override void EventSourceOnDisposeCalled(object sender, EventArgs eventArgs)
        {
            ChildOwner.ClearOwnedSubIndicies();
            base.EventSourceOnDisposeCalled(sender, eventArgs);
        }
    }
}