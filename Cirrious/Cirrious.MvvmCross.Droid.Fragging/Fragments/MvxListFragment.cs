using System;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.App;
using Android.Views;
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using System.Collections.Generic;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;

namespace Cirrious.MvvmCross.Droid.Fragging.Fragments
{
    public class MvxListFragment : MvxFragment
    {
        private ViewGroup _layout;

        public MvxListFragment(Activity activity, MvxListViewResources listViewResources = null, Dictionary<int, string> listItemBindings = null) : base(activity) 
        {
            if (listViewResources != null) ListViewResources = listViewResources;
            if (listItemBindings != null) ListViewItemBindings = listItemBindings;
        }

        private MvxListView _listView;
        public MvxListView ListView
        {
            get { return _listView; }
            set { _listView = value; }
        }                        

        private MvxListViewResources _listViewResources;
        public MvxListViewResources ListViewResources
        {
            get { return _listViewResources; }
            set { _listViewResources = value; }
        }

        private Dictionary<int, string> _listViewItemBindings;
        public Dictionary<int, string> ListViewItemBindings
        {
            get
            {
                if (_listViewItemBindings == null)
                {
                    _listViewItemBindings = new Dictionary<int, string>();
                }
                return _listViewItemBindings;
            }
            set { _listViewItemBindings = value; }
        }            

        public void AddListViewItemBinding(int viewId, string bindingDescription)
        {
            ListViewItemBindings.Add(viewId, bindingDescription);
        }

        public void SetListViewResources(int container, int list, int item)
        {
            ListViewResources = new MvxListViewResources(container, list, item);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _layout = (ViewGroup)BindingInflate(ListViewResources.Container);
            ListView = _layout.FindViewById<MvxListView>(ListViewResources.List);
            ListView.ItemTemplateId = ListViewResources.Item;

            IMvxBindingDescriptionContainer bindingDescriptionContainer = Activity as IMvxBindingDescriptionContainer;
            if (bindingDescriptionContainer != null)
            {
                foreach (KeyValuePair<int, string> entry in ListViewItemBindings)
                {
                    if (!bindingDescriptionContainer.BindingDescriptions.ContainsKey(entry.Key))
                    {
                        bindingDescriptionContainer.BindingDescriptions.Add(entry.Key, entry.Value);
                    }
                }
            }

            return _layout;
        }           

        public class MvxListViewResources
        {
            public int _container;
            public int Container
            {
                get { return _container; }
                set { _container = value; }
            }

            public int _list;
            public int List
            {
                get { return _list; }
                set { _list = value; }
            }

            public int _item;
            public int Item
            {
                get { return _item; }
                set { _item = value; }
            }

            public MvxListViewResources()
            {
            }

            public MvxListViewResources(int container, int list, int item)
            {
                Container = container;
                List = list;
                Item = item;
            }
        }
    }
}

