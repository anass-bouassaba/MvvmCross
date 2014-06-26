// MvxAndroidViewPresenter.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Android.App;
using Android.Content;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Android.Support.V4.App;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentActivity = Android.Support.V4.App.FragmentActivity;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace Cirrious.MvvmCross.Droid.Views
{
    public enum ViewCategory
    {
        Unknown = 0,
        Activity = 1,
        Fragment = 2
    };

    public struct ViewDetails
    {
        public ViewCategory category;
        public Type type;
    }

    public class MvxAndroidViewPresenter
        : IMvxAndroidViewPresenter
    {
        protected Activity Activity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        public virtual void Show(MvxViewModelRequest request)
        {   
            ViewDetails viewDetails = IdentifyView(request);
            if (viewDetails.category == ViewCategory.Activity)
            {
                var intent = CreateIntentForRequest(request);
                Show(intent);
            }
            else if (viewDetails.category == ViewCategory.Fragment)
            {
//                IMvxChildViewContainer fragmentActivity = Activity as IMvxChildViewContainer;
//                if (fragmentActivity != null)
//                {
//                    Fragment currentFragment = (Fragment)fragmentActivity.CurrentChildView;
//                    IMvxParameterValuesContainer newFragment = (IMvxParameterValuesContainer)Activator.CreateInstance(viewDetails.type, fragmentActivity);
//                    newFragment.ParameterValues = request.ParameterValues;
//
//                    FragmentTransaction trans = currentFragment.FragmentManager.BeginTransaction();
//                    IMvxViewGroupContainer rootFragment = currentFragment as IMvxViewGroupContainer;
//                    if (rootFragment != null)
//                    {
//                        Fragment fragmentToReplace = newFragment as Fragment;
//                        trans.Replace(rootFragment.Layout.Id, fragmentToReplace);
//                        trans.AddToBackStack(null);
//                        trans.Commit();
//
//                        IMvxParameterValuesStackContainer stackContainer = currentFragment as IMvxParameterValuesStackContainer;
//                        if (stackContainer != null)
//                        {
//                            stackContainer.Stack.Push(newFragment);
//                        }
//                    }
//                }
            }
        }

        protected ViewDetails IdentifyView(MvxViewModelRequest request)
        {
            var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
            MvxAndroidViewsContainer container = requestTranslator as MvxAndroidViewsContainer;
            return container.IdentifyView(request);
        }

        protected virtual void Show(Intent intent)
        {
            var activity = Activity;
            if (activity == null)
            {
                MvxTrace.Warning("Cannot Resolve current top activity");
                return;
            }
            activity.StartActivity(intent);
        }

        protected virtual Intent CreateIntentForRequest(MvxViewModelRequest request)
        {
            var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
            var intent = requestTranslator.GetIntentFor(request);
            return intent;
        }

        public virtual void ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxClosePresentationHint)
            {
                Close((hint as MvxClosePresentationHint).ViewModelToClose);
                return;
            }

            MvxTrace.Warning("Hint ignored {0}", hint.GetType().Name);
        }

        public virtual void Close(IMvxViewModel viewModel)
        {
            var activity = Activity;

            var currentView = activity as IMvxView;

            if (currentView == null)
            {
                Mvx.Warning("Ignoring close for viewmodel - rootframe has no current page");
                return;
            }

            if (currentView.ViewModel != viewModel)
            {
                Mvx.Warning("Ignoring close for viewmodel - rootframe's current page is not the view for the requested viewmodel");
                return;
            }

            activity.Finish();
        }
    }
}