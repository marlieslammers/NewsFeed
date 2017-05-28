using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsFeed.Helpers
{
    public static class ApplicationProperties
    {
        public static T GetProperty<T>(string propertyName, object defaultValue)
        {
            if (!Application.Current.Properties.ContainsKey(propertyName))
            {
                SetProperty<T>(propertyName, defaultValue);
            }

            return (T)Convert.ChangeType(Application.Current.Properties[propertyName], typeof(T));
        }

        public static void SetProperty<T>(string propertyName, object value)
        {
            Application.Current.Properties[propertyName] = (T)Convert.ChangeType(value, typeof(T));
        }

        public static class PropertyNames
        {
            public static readonly string Feed = "Feed";
        }

        public static class DefaultValues
        {
            public static readonly string Feed = "http://www.nu.nl/rss/Algemeen";
        }
    }
}
