using System;
using System.Collections.Generic;
using System.Text;

namespace Horus.Models
{
	public class Challenge
	{
		public string id { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public int currentPoints { get; set; }
		public int totalPoints { get; set; }
		public float percentage 
		{
			get
			{
				var percen  = (currentPoints * 100) / totalPoints;
				return Convert.ToInt32(percen);
			}
		}
		public float progress
		{
			get
			{
				return percentage / 100;
			}
		}
	}

}
