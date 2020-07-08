using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace KnowledgeBase.Converters
{
    public class TagNameToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tagName = value.ToString();

            var tagBrush = TagColors.GetTagColor(tagName);

            return tagBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static class TagColors
    {
        private static readonly Dictionary<string, SolidColorBrush> tagColors = new Dictionary<string, SolidColorBrush>();
        private static readonly Random random = new Random();

        public static SolidColorBrush GetTagColor(string tagName)
        {
            tagColors.TryGetValue(tagName, out SolidColorBrush brush);

            if (brush == null)
            {
                byte r = Convert.ToByte(random.Next(180, 255));
                var g = Convert.ToByte(random.Next(180, 255));
                var b = Convert.ToByte(random.Next(180, 255));

                var tagColor = new SolidColorBrush(Color.FromArgb(255, r, g, b));

                tagColors.Add(tagName, tagColor);

                brush = tagColor;
            }

            return brush;
        }
    }
}
