using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;

namespace Tides
{
    public class TideAdapter : SimpleAdapter, ISectionIndexer
    {
        IList<IDictionary<string, object>> dataList;
        string[] sections;
        Dictionary<string, int> alphaIndex;
        Java.Lang.Object[] sectionObjects;

        public TideAdapter(Context context, IList<IDictionary<string, object>> data, int resource, string[] from, int[] to)
            : base(context, data, resource, from, to)
        {
            dataList = data;
            BuildSectionIndex();
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionObjects;
        }

        public int GetPositionForSection(int section)
        {
            return alphaIndex[sections[section]];
        }

        public int GetSectionForPosition(int position)
        {
            return 1;
        }

        private void BuildSectionIndex()
        {
            alphaIndex = new Dictionary<string, int>();

            for(int i = 0; i < Count; i++)
            {
                var date = DateTime.Parse((string)dataList[i]["date"]);
                var key = date.ToString("MMM");

                if (!alphaIndex.ContainsKey(key))
                {
                    alphaIndex[key] = i;
                }
            }

            sections = new string[alphaIndex.Keys.Count];
            alphaIndex.Keys.CopyTo(sections, 0);

            sectionObjects = new Java.Lang.Object[sections.Length];
            for(int i = 0; i < sections.Length; i++)
            {
                sectionObjects[i] = new Java.Lang.String(sections[i]);
            }
        }
    }
}