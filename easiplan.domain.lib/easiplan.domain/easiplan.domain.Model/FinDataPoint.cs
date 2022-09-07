namespace easiplan.domain.Model
{
	public class FinDataPoint
	{
		public double X
		{
			get;
			set;
		}

		public double Y
		{
			get;
			set;
		}

		public string Label
		{
			get;
			set;
		}

		public FinDataPoint(double x, double y, string label = "")
		{
			X = x;
			Y = y;
			Label = label;
		}
	}
}
