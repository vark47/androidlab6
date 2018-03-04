using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Tides
{
    [Activity(Label = "Tides", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);




            // pulls and parses xml data
            var parser = new TideXMLParser(Assets.Open("9434032_annual.xml"));
            var tideList = parser.ParsedTides;

            ListAdapter = new TideAdapter(this, tideList, Android.Resource.Layout.TwoLineListItem, new string[] { "daydate", "tidetime" }, 
                new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });





            //TODO fast scrolling
            ListView.FastScrollEnabled = true;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //event handler for toast
            var height = (string)((JavaDictionary<string, object>)ListView.GetItemAtPosition(position))["feet"] + " feet";
            Toast.MakeText(this, height, ToastLength.Long).Show();
        }
    }
}

