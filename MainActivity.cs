using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Nio.FileNio;
using System.IO;
using Xamarin.Essentials;
using FileSystem = Xamarin.Essentials.FileSystem;

namespace LauncherEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnClick;
        private Button btnFileOpen;
        private WebView webView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnClick = FindViewById<Button>(Resource.Id.btnClick);
            btnFileOpen = FindViewById<Button>(Resource.Id.btnFileOpen);
            webView = FindViewById<WebView>(Resource.Id.webView);

            btnClick.Click += BtnClick_Click;
            btnFileOpen.Click += BtnFileOpen_Click;
        }

        private async  void BtnFileOpen_Click(object sender, System.EventArgs e)
        {
            var fn = "Attech.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");

            await Launcher.OpenAsync(new OpenFileRequest
            {

                File = new ReadOnlyFile(file)
            });
        }

        private async  void BtnClick_Click(object sender, System.EventArgs e)
        {
            var supportsUri = await Launcher.CanOpenAsync("https://docs.microsoft.com/en-us/xamarin/essentials/");

            if (supportsUri)
            {
                await Launcher.OpenAsync("https://docs.microsoft.com/en-us/xamarin/essentials/");
            }
        }
    }
}