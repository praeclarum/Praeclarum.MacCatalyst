using Foundation;
using System;
using UIKit;

namespace Hello.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var label = new UILabel {
                Text = "Hello Cat Frank",
                Font = UIFont.SystemFontOfSize (44),
            };
            label.Frame = View.Bounds;
            View.AddSubview (label);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}