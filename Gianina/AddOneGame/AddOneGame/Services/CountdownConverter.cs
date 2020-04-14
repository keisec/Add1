using System;
using Xamarin.Forms;

namespace AddOneGame.Services
{
    /// <summary>
    /// Converts countdown seconds double value to string "HH : MM : SS"
    /// </summary>
    public class CountdownConverter
    {
        #region IValueConverter implementation

        public static string Convert(double value)
        {
            var timespan = TimeSpan.FromSeconds((double)value);

            if (timespan.TotalSeconds < 1.0)
            {
                return "-- : -- : --";
            }
            //            else if (timespan.TotalSeconds < 3600)
            //            {
            //                return string.Format("{0:D2} : {1:D2}",
            //                    timespan.Minutes, timespan.Seconds);
            //            }
            else if (timespan.TotalSeconds > 3600 * 24)
            {
                return "24 : 00 : 00 : 00";
            }

            return string.Format("{1:D2} : {2:D2} : {3:D2}",
                timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}