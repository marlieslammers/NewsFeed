namespace NewsFeed.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new NewsFeed.App());
        }
    }
}