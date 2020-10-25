using Foundation;
using System;
using UIKit;

namespace Hello.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
			Console.WriteLine ("Created ViewController");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Console.WriteLine ("ViewController ViewLoaded");
            // Perform any additional setup after loading the view, typically from a nib.
            var label = new UILabel {
                Text = "Hello James from Mac Catalyst",
                Font = UIFont.SystemFontOfSize (44),
            };
            label.Frame = View.Bounds;
            label.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            label.BackgroundColor = UIColor.Blue;
            View.AddSubview (label);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}