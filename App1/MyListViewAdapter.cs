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

namespace App1
{
    class MyListViewAdapter : BaseAdapter<KeyValuePair<string, double>>
    {
        private  List<KeyValuePair<string, double>> fuel;
        private Context mContext;

        public MyListViewAdapter(Context _contect, List<KeyValuePair<string,double>> _fuel)
        {
            this.fuel = _fuel;
            mContext = _contect;
        }

        public override int Count
        {
           get
            {
            return fuel.Count;
             }
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override KeyValuePair<string, double> this[int position]
        {
            get
            {
                return fuel[position];
            }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }
          
            TextView textName = row.FindViewById<TextView>(Resource.Id.textViewName);
            textName.Text = fuel[position].Key;

            TextView textCount = row.FindViewById<TextView>(Resource.Id.textViewCount);
            textCount.Text = string.Format("{0:N2}", fuel[position].Value);
            return row;
        }
    }
}