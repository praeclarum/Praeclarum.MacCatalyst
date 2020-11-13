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
			Console.WriteLine (new SkiaSharp.Views.Forms.SKCanvasView ());
			//Console.WriteLine (Microsoft.AppCenter.AppCenter.SdkVersion);
		}
    }
}
