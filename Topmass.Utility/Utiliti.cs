using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Topmass.Utility
{
    public static class Utilities
    {

        private static string[] VietnameseSigns = new string[]
           {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
           };

        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }


        public static string SlugifySlug(string input)
        {

            if (string.IsNullOrEmpty(input))
                return string.Empty;
            // Normalize the input string to decompose accented characters
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            // Remove diacritic marks (accents) and convert to ASCII
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            // Convert to a string without diacritics
            string asciiString = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Convert to lowercase
            asciiString = asciiString.ToLowerInvariant();

            // Remove invalid characters
            asciiString = Regex.Replace(asciiString, @"[^a-z0-9\s-]", "");

            // Replace spaces and repeated dashes with a single dash
            asciiString = Regex.Replace(asciiString, @"[\s-]+", " ").Trim();
            asciiString = Regex.Replace(asciiString, @"\s", "-");

            return asciiString;
        }

        public static string SlugifySlug2(string value)
        {
            return SlugifySlug(value);
        }
    }
}
