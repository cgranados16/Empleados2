using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace EmpleadosUWP.ValuesConverter
{
    /// <summary>
    /// Applies format strings and culture information to string conversions. 
    /// </summary>
    public class StringFormatterConverter : IValueConverter
    {
        /// <summary>
        /// Converts objects to strings.  
        /// </summary>
        /// <param name="value">The object to convert. This can be any object.</param>
        /// <param name="targetType">The type to convert to. This should be the string type.</param>
        /// <param name="parameter">The format string.</param>
        /// <param name="language">Language and culture info. If this is null, we use the current culture.</param>
        /// <returns>The converted object.</returns>
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {

            if (targetType.Equals(typeof(System.String)))
            {
                
                // Retrieve the format string and use it to format the value.
                string formatString = parameter as string;
                if (!string.IsNullOrEmpty(formatString))
                {

                    CultureInfo culture = (!string.IsNullOrEmpty(language)) ? new CultureInfo(language) : CultureInfo.CurrentCulture;
                    return string.Format(culture, formatString, value);
                }

                // If the format string is null or empty, simply call ToString()
                // on the value.
                return value.ToString();
            }
            else
            {
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
