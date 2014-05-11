// MvxActivityViewExtensions.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Android.App;
using Android.OS;
using Cirrious.CrossCore.Droid.Views;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Android.Support.V4.App;
using Fragment = Android.Support.V4.App.Fragment;
using Cirrious.MvvmCross.Droid.Fragging.EventSource;
using Cirrious.MvvmCross.Droid.Fragging;

namespace Cirrious.MvvmCross.Droid.Views
{
    public static partial class MvxActivityViewExtensions
    {
        public static void AddEventListeners(this IMvxEventSourceFragmentActivity fragmentActivity)
        {
            if (fragmentActivity is IMvxAndroidView)
            {
                var adapter = new MvxFragmentActivityAdapter(fragmentActivity);
            }
            if (fragmentActivity is IMvxBindingContextOwner)
            {
                var bindingAdapter = new MvxBindingFragmentActivityAdapter(fragmentActivity);
            }
            if (fragmentActivity is IMvxChildViewModelOwner)
            {
                var childOwnerAdapter = new MvxChildViewModelOwnerAdapterFragmentActivity(fragmentActivity);
            }
        }
    }
}