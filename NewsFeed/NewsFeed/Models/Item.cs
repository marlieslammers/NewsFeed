using System;

namespace NewsFeed.Models
{
    public class Item : BaseDataObject
    {
        string title = string.Empty;
        string description = string.Empty;
        Uri link;
        DateTime pubDate;
        Uri encloseUrl;
        string age;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public Uri Link
        {
            get { return link; }
            set { SetProperty(ref link, value); }
        }

        public DateTime PubDate
        {
            get { return pubDate; }
            set { SetProperty(ref pubDate, value); }
        }

        public Uri EncloseUrl
        {
            get { return encloseUrl; }
            set { SetProperty(ref encloseUrl, value); }
        }

        public string Age
        {
            get
            {
                if (PubDate != DateTime.MinValue)
                {
                    var minutes = (DateTime.Now - PubDate).TotalMinutes;
                    const int hour = 60;
                    const int day = 1440;

                    if (minutes < hour)
                    {
                        return $"{Math.Round(minutes)} m";
                    }
                    else if (minutes < day)
                    {
                        return $"{Math.Round(minutes / hour)} h";
                    }
                    else
                    {
                        return $"{Math.Round(minutes / day)} d";
                    }
                }

                return string.Empty;
            }
        }
    }
}
