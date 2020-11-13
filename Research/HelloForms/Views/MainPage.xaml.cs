using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloForms.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var v = new SkiaSharp.Views.Forms.SKCanvasView ();
			Console.WriteLine (v);
            Console.WriteLine (Microsoft.AppCenter.AppCenter.SdkVersion);
        }
    }
}
