// MvxFragmentActivity.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.View;
using Android.Widget;
using Android.Views;
using Android.OS;
using Cirrious.CrossCore.Droid.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Cirrious.MvvmCross.Droid.Fragging
{
    public abstract class MvxViewPagerActivity
        : MvxFragmentActivity
          , IMvxChildViewContainer
    {
        protected MvxViewPagerActivity() : base()
        {
            Adapter = new MvxViewPagerAdapter(SupportFragmentManager, this);
        }

        MvxViewPagerAdapter _adapter;
        public MvxViewPagerAdapter Adapter
        {
            get { return _adapter; }
            set { _adapter = value; }
        }

        ViewPager _viewPager;
        public ViewPager ViewPager
        {
            get { return _viewPager; }
            set { _viewPager = value; }
        }

        List<MvxRootFragment> _rootFragments;
        public List<MvxRootFragment> RootFragments
        {
            get
            {
                if (_rootFragments == null)
                {
                    _rootFragments = new List<MvxRootFragment>();
                }
                return _rootFragments;
            }
            set { _rootFragments = value; }
        }

        #region IMvxChildViewContainer implementation
        public IMvxViewGroupContainer CurrentChildView
        {
            get
            {
                if (RootFragments.Count == 0)
                    throw new Exception("No child root fragments found");
                if (_viewPager == null)
                    throw new Exception("No view pager found");

                return RootFragments[ViewPager.CurrentItem] as MvxRootFragment;
            }
        }
        #endregion


        public void AddPage(MvxFragment f, int iconId, string title, int containerId)
        {
            MvxRootFragment rf = new MvxRootFragment();
            rf.IconId = iconId;
            rf.Title = title;
            rf.ContainerId = containerId;
            rf.Stack.Push(f);

            RootFragments.Add(rf);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId) {
                // Respond to the action bar's Up/Home button
                case Android.Resource.Id.Home:
                    MvxRootFragment currentRootFragment = CurrentChildView as MvxRootFragment;
                    currentRootFragment.Stack.Pop();
                    OnBackPressed();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        /**
         * The following class makes sense only for an MvxViewPagerActivity
         */
        public class MvxRootFragment 
            : Fragment
              , IMvxViewGroupContainer
        , IMvxParameterValuesStackContainer
        {
            public MvxRootFragment() : base()
            {
            }
                
            public MvxViewPagerActivity ViewPagerActivity
            {
                get { return Activity as MvxViewPagerActivity; }
            }
                
            #region IMvxRootFragment implementation
            ViewGroup _layout;
            public ViewGroup Layout
            {
                get { return _layout; }
                set { _layout = value; }
            }
            #endregion

            string _title;
            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _iconId;
            public int IconId
            {
                get { return _iconId; }
                set { _iconId = value; }
            }

            int _containerId;
            public int ContainerId
            {
                get { return _containerId; }
                set { _containerId = value; }
            }                

            #region IMvxViewGroupStackContainer implementation
            Stack<IMvxParameterValuesContainer> _stack;
            public Stack<IMvxParameterValuesContainer> Stack
            {
                get
                {
                    if (_stack == null)
                    {
                        _stack = new Stack<IMvxParameterValuesContainer>();
                    }
                    return _stack;
                }
                set { _stack = value; }
            }
            #endregion

            public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                Layout = (LinearLayout)inflater.Inflate(ContainerId, container, false);

                FragmentTransaction ft = FragmentManager.BeginTransaction();
                Fragment firstChildFragment = (Fragment)Stack.Peek();
                ft.Replace(_layout.Id, firstChildFragment);
                ft.Commit();

                return Layout;
            }
        }
    }
}