using System;

using UIKit;
using CoreGraphics;

using System.Security.Cryptography;
using Elliptic;

namespace EccDHIosC
{
    public partial class ViewController : UIViewController
    {
        byte[] aliceRandomBytes;
        byte[] alicePrivateBytes;
        byte[] alicePublicBytes;

        byte[] bobRandomBytes;
        byte[] bobPrivateBytes;
        byte[] bobPublicBytes;

        byte[] aliceBobSharedBytes;
        byte[] bobAliceSharedBytes;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
            var viewBoundsWidth = View.Bounds.Width;

            var scrollView = new UIScrollView (new CGRect (0, 0, View.Frame.Width, View.Frame.Height));
            View.AddSubview (scrollView);

            var yValue = 10;
            var height = 40;
            var padding = 0;

            var a1b = UIButton.FromType(UIButtonType.RoundedRect);
            a1b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            a1b.SetTitle ("Alice: Generates Random Key", UIControlState.Normal);
            a1b.SetTitle ("Computing", UIControlState.Highlighted);
            a1b.SetTitle ("Alice: Generates Random Key", UIControlState.Disabled);
            a1b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            a1b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            a1b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var AliceRandomText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Cyan,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var a2b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            a2b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            a2b.SetTitle ("Alice: Computes Private Key from Random Key", UIControlState.Normal);
            a2b.SetTitle ("Computing", UIControlState.Highlighted);
            a2b.SetTitle ("Alice: Computes Private Key from Random Key", UIControlState.Disabled);
            a2b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            a2b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            a2b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var AlicePrivateKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Cyan,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var a3b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            a3b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            a3b.SetTitle ("Alice: Computes Public Key from Random Key", UIControlState.Normal);
            a3b.SetTitle ("Computing", UIControlState.Highlighted);
            a3b.SetTitle ("Alice: Computes Public Key from Random Key", UIControlState.Disabled);
            a3b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            a3b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            a3b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var AlicePublicKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Cyan,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            yValue = yValue + height + padding;
            var b1b = UIButton.FromType(UIButtonType.RoundedRect);
            b1b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            b1b.SetTitle ("Bob: Generates Random Key", UIControlState.Normal);
            b1b.SetTitle ("Computing", UIControlState.Highlighted);
            b1b.SetTitle ("Bob: Generates Random Key", UIControlState.Disabled);
            b1b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            b1b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            b1b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var BobRandomText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Yellow,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var b2b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            b2b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            b2b.SetTitle ("Bob: Computes Private Key from Random Key", UIControlState.Normal);
            b2b.SetTitle ("Computing", UIControlState.Highlighted);
            b2b.SetTitle ("Bob: Computes Private Key from Random Key", UIControlState.Disabled);
            b2b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            b2b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            b2b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var BobPrivateKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Yellow,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var b3b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            b3b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            b3b.SetTitle ("Bob: Computes Public Key from Random Key", UIControlState.Normal);
            b3b.SetTitle ("Computing", UIControlState.Highlighted);
            b3b.SetTitle ("Bob: Computes Public Key from Random Key", UIControlState.Disabled);
            b3b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            b3b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            b3b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var BobPublicKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Yellow,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var a4b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            a4b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            a4b.SetTitle ("Alice: Computes shared Key with her private and Bobs Public Keys", UIControlState.Normal);
            a4b.SetTitle ("Computing", UIControlState.Highlighted);
            a4b.SetTitle ("Alice: Computes shared Key with her private and Bobs Public Keys", UIControlState.Disabled);
            a4b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            a4b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            a4b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var AliceBobSharedKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Cyan,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            var b4b = UIButton.FromType(UIButtonType.RoundedRect);
            yValue = yValue + height + padding;
            b4b.Frame = new CGRect (0, yValue, viewBoundsWidth, height);
            b4b.SetTitle ("Bob: Computes shared Key with his private and Alices Public Keys", UIControlState.Normal);
            b4b.SetTitle ("Computing", UIControlState.Highlighted);
            b4b.SetTitle ("Bob: Computes shared Key with his private and Alices Public Keys", UIControlState.Disabled);
            b4b.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
            b4b.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            b4b.TitleLabel.TextAlignment = UITextAlignment.Center;

            yValue = yValue + height + padding;
            var BobAliceSharedKeyText = new UITextView
            {
                Editable = false,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Yellow,
                Frame = new CGRect(0, yValue, viewBoundsWidth, height)
            };

            // Button callbacks
            a1b.TouchDown += delegate {
                alicePrivateBytes = null;
                AlicePrivateKeyText.Text = "";

                alicePublicBytes = null;
                AlicePublicKeyText.Text = "";
                a2b.Enabled = true;
                a3b.Enabled = false;
                a4b.Enabled = false;
                b4b.Enabled = false;

                aliceBobSharedBytes = null;
                bobAliceSharedBytes = null;

                AliceBobSharedKeyText.Text = "";
                BobAliceSharedKeyText.Text = "";

                aliceRandomBytes = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(aliceRandomBytes);
                AliceRandomText.Text = BitConverter.ToString(aliceRandomBytes).Replace("-","");
            };

            a2b.TouchDown += delegate {
                if (aliceRandomBytes != null)
                {
                    alicePrivateBytes = Curve25519.ClampPrivateKey(aliceRandomBytes);
                    AlicePrivateKeyText.Text = BitConverter.ToString(alicePrivateBytes).Replace("-","");
                    a3b.Enabled = true;
                    if ( (alicePrivateBytes != null) && (bobPublicBytes != null) ) {
                        a4b.Enabled = true;
                    }
                }
            };

            a3b.TouchDown += delegate {
                if (alicePrivateBytes != null)
                {
                    alicePublicBytes = Curve25519.GetPublicKey(alicePrivateBytes);
                    AlicePublicKeyText.Text = BitConverter.ToString(alicePublicBytes).Replace("-","");
                    if ( (bobPrivateBytes != null) && (alicePublicBytes != null) ) {
                        b4b.Enabled = true;
                    }
                }
            };

            b1b.TouchDown += delegate {
                bobPrivateBytes = null;
                BobPrivateKeyText.Text = ""; // Reset

                bobPublicBytes = null;
                BobPublicKeyText.Text = ""; // Reset
                b2b.Enabled = true;
                b3b.Enabled = false;
                b4b.Enabled = false;
                a4b.Enabled = false;

                aliceBobSharedBytes = null;
                bobAliceSharedBytes = null;

                AliceBobSharedKeyText.Text = "";
                BobAliceSharedKeyText.Text = "";

                bobRandomBytes = new byte[32];
                RNGCryptoServiceProvider.Create().GetBytes(bobRandomBytes);
                BobRandomText.Text = BitConverter.ToString(bobRandomBytes).Replace("-","");
            };

            b2b.TouchDown += delegate {
                if (bobRandomBytes != null)
                {
                    bobPrivateBytes = Curve25519.ClampPrivateKey(bobRandomBytes);
                    BobPrivateKeyText.Text = BitConverter.ToString(bobPrivateBytes).Replace("-","");
                    b3b.Enabled = true;
                    if ( (bobPrivateBytes != null) && (alicePublicBytes != null) ) {
                        b4b.Enabled = true;
                    }
                }
            };

            b3b.TouchDown += delegate {
                if (bobPrivateBytes != null)
                {
                    bobPublicBytes = Curve25519.GetPublicKey(bobPrivateBytes);
                    BobPublicKeyText.Text = BitConverter.ToString(bobPublicBytes).Replace("-","");
                    if ( (alicePrivateBytes != null) && (bobPublicBytes != null) ) {
                        a4b.Enabled = true;
                    }
                }
            };

            a4b.TouchDown += delegate {
                if ( (alicePrivateBytes != null) && (bobPublicBytes != null) )
                {
                    aliceBobSharedBytes = Curve25519.GetSharedSecret(alicePrivateBytes, bobPublicBytes);
                    AliceBobSharedKeyText.Text = BitConverter.ToString(aliceBobSharedBytes).Replace("-","");
                }

            };

            b4b.TouchDown += delegate {
                if ( (bobPrivateBytes != null) && (alicePublicBytes != null) )
                {
                    bobAliceSharedBytes = Curve25519.GetSharedSecret(bobPrivateBytes, alicePublicBytes);
                    BobAliceSharedKeyText.Text = BitConverter.ToString(bobAliceSharedBytes).Replace("-","");
                }
            };

            a2b.Enabled = false;
            a3b.Enabled = false;
            a4b.Enabled = false;

            b2b.Enabled = false;
            b3b.Enabled = false;
            b4b.Enabled = false;

            scrollView.AddSubview (a1b);
            scrollView.AddSubview (AliceRandomText);

            scrollView.AddSubview (a2b);
            scrollView.AddSubview (AlicePrivateKeyText);

            scrollView.AddSubview (a3b);
            scrollView.AddSubview (AlicePublicKeyText);

            scrollView.AddSubview (b1b);
            scrollView.AddSubview (BobRandomText);

            scrollView.AddSubview (b2b);
            scrollView.AddSubview (BobPrivateKeyText);

            scrollView.AddSubview (b3b);
            scrollView.AddSubview (BobPublicKeyText);

            scrollView.AddSubview (a4b);
            scrollView.AddSubview (AliceBobSharedKeyText);

            scrollView.AddSubview (b4b);
            scrollView.AddSubview (BobAliceSharedKeyText);

            var contentHeight = yValue + 20;
            scrollView.ContentSize = new CGSize(viewBoundsWidth, contentHeight);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
