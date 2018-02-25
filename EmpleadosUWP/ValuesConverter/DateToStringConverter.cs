using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace EmpleadosUWP.ValuesConverter
{ 
    class DateToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts the DateTime object to the string to display. 
        /// </summary>
        /// <param name="value">The DateTime object.</param>
        /// <param name="targetType">The type to convert to. This should be string.</param>
        /// <param name="parameter">The format string.</param>
        /// <param name="language">Language and culture info. If this is null, we use the current culture.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {

            var date = value as DateTime?;

            if (targetType.Equals(typeof(System.String)))
            {
                // Retrieve the format string and use it to format the value.
                string formatString = parameter as string;
                if (!string.IsNullOrEmpty(formatString))
                {

                    CultureInfo culture = (!string.IsNullOrEmpty(language)) ? new CultureInfo(language) : CultureInfo.CurrentCulture;
                    return date.Value.ToString(formatString, culture);
                }

                // If the format string is null or empty, simply call ToString()
                // on the value.
                return value.ToString();
            }
            else
            {
                //return DateTimeOffset.Now;
                throw new ArgumentException($"Unsuported type: {targetType.FullName}");
            }

        }

        /// <summary>
        /// No need to implement converting back on a one-way binding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
