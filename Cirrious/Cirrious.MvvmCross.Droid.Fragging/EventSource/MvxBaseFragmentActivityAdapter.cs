﻿// MvxBaseActivityAdapter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Droid.Fragging.Fragments.EventSource;
using Cirrious.CrossCore.Droid.Views;

namespace Cirrious.MvvmCross.Droid.Fragging.EventSource
{
    public class MvxBaseFragmentActivityAdapter
    {
        private readonly IMvxEventSourceFragmentActivity _eventSource;

        protected Activity Activity
        {
            get { return _eventSource as Activity; }
        }

        public MvxBaseFragmentActivityAdapter(IMvxEventSourceFragmentActivity eventSource)
        {
            _eventSource = eventSource;

            _eventSource.CreateCalled += EventSourceOnCreateCalled;
            _eventSource.CreateWillBeCalled += EventSourceOnCreateWillBeCalled;
            _eventSource.StartCalled += EventSourceOnStartCalled;
            _eventSource.RestartCalled += EventSourceOnRestartCalled;
            _eventSource.ResumeCalled += EventSourceOnResumeCalled;
            _eventSource.PauseCalled += EventSourceOnPauseCalled;
            _eventSource.StopCalled += EventSourceOnStopCalled;
            _eventSource.DestroyCalled += EventSourceOnDestroyCalled;
            _eventSource.DisposeCalled += EventSourceOnDisposeCalled;
            _eventSource.SaveInstanceStateCalled += EventSourceOnSaveInstanceStateCalled;
            _eventSource.NewIntentCalled += EventSourceOnNewIntentCalled;

            _eventSource.ActivityResultCalled += EventSourceOnActivityResultCalled;
            _eventSource.StartActivityForResultCalled += EventSourceOnStartActivityForResultCalled;
        }

        protected virtual void EventSourceOnSaveInstanceStateCalled(object sender,
            MvxValueEventArgs<Bundle> mvxValueEventArgs)
        {
        }

        protected virtual void EventSourceOnCreateWillBeCalled(object sender,
            MvxValueEventArgs<Bundle> MvxValueEventArgs)
        {
        }

        protected virtual void EventSourceOnStopCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnStartCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnStartActivityForResultCalled(object sender,
            MvxValueEventArgs
            <MvxStartActivityForResultParameters>
            MvxValueEventArgs)
        {
        }

        protected virtual void EventSourceOnResumeCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnRestartCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnPauseCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnNewIntentCalled(object sender, MvxValueEventArgs<Intent> MvxValueEventArgs)
        {
        }

        protected virtual void EventSourceOnDisposeCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnDestroyCalled(object sender, EventArgs eventArgs)
        {
        }

        protected virtual void EventSourceOnCreateCalled(object sender, MvxValueEventArgs<Bundle> MvxValueEventArgs)
        {
        }

        protected virtual void EventSourceOnActivityResultCalled(object sender,
            MvxValueEventArgs<MvxActivityResultParameters>
            MvxValueEventArgs)
        {
        }
    }
}