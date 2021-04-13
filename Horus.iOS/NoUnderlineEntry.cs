using Foundation;
using Horus.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Entry),typeof(NoUnderlineEntry))]
namespace Horus.iOS
{
	public class NoUnderlineEntry:EntryRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}