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
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Droid.Fragging.Fragments.EventSource;

namespace Cirrious.MvvmCross.Droid.Views
{
    public static partial class MvxActivityViewExtensions
    {
        public static void AddEventListeners(this IMvxEventSourceFragment fragment)
        {
            if (fragment is IMvxAndroidView)
            {
                var adapter = new MvxFragmentAdapter(fragment);
            }
            if (fragment is IMvxBindingContextOwner)
            {
                var bindingAdapter = new MvxBindingFragmentAdapter(fragment);
            }
            if (fragment is IMvxChildViewModelOwner)
            {
                var childOwnerAdapter = new MvxChildViewModelOwnerAdapterFragment(fragment);
            }
        }
    }
}