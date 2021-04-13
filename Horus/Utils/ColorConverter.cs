using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Horus.Utils
{
	public class ColorConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.ToString() == "100")
			{
				if ((string)parameter == "arrow")
					return "arrow_right_w.png";
				if ((string)parameter == "frame")
					return "#F49390";
				return "#FFFFFF";
			}
			else
			{
				if ((string)parameter == "frame")
					return "#FFFFFF";
				if ((string)parameter == "arrow")
					return "arrow_right_g.png";
				if ((string)parameter == "title")
					return "#1A1A1A";
				if ((string)parameter == "progress")
					return "#F49390";
				if ((string)parameter == "percentage")
					return "#F49390";
				else
					return "#828282";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool)value;
		}
	}
}
