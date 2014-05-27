﻿// MvxFragmentActivity.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentPagerAdapter = Android.Support.V4.App.FragmentPagerAdapter;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace Cirrious.MvvmCross.Droid.Fragging
{
    public class MvxViewPagerAdapter : FragmentPagerAdapter
    {
        int _primaryItemPosition;

        protected MvxViewPagerActivity _viewPagerActivity;
        public MvxViewPagerActivity ViewPagerActivity
        {
            get { return _viewPagerActivity; }
            set { _viewPagerActivity = value; }
        }

        public MvxViewPagerAdapter (FragmentManager fm, MvxViewPagerActivity viewPagerActivity) : base(fm)
        {
            _viewPagerActivity = viewPagerActivity;
        }

        public override Fragment GetItem (int position)
        {
            if (ViewPagerActivity.RootFragments.Count == 0)
            {
                throw new Exception("There is no Fragments to show on the ViewPager");
            }
            return ViewPagerActivity.RootFragments[position];
        }

        public virtual string GetTitle(int position)
        {
            return ViewPagerActivity.RootFragments[position].Title;
        }

        public virtual int GetIconId(int position)
        {
            return ViewPagerActivity.RootFragments[position].IconId;
        }

        public override int Count {
            get
            {
                return ViewPagerActivity.RootFragments.Count;
            }
        }

        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PositionNone;
        }

        public override void SetPrimaryItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            base.SetPrimaryItem(container, position, @object);
            _primaryItemPosition = position;
        }

        public override void FinishUpdate(ViewGroup container)
        {
            base.FinishUpdate(container);

            MvxViewPagerActivity.MvxRootFragment rootFragment = GetItem(_primaryItemPosition) as MvxViewPagerActivity.MvxRootFragment;
            if (rootFragment != null) {
                INotifyViewPagerItemChanged childFragment = rootFragment.Stack.Peek() as INotifyViewPagerItemChanged;
                if (childFragment != null) {
                    childFragment.FinishUpdate();
                }
            }
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object @object) {}
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object) {}
    }
}

