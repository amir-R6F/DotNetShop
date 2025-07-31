using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Shop.Application
{
    public static class SlugGenerator
    {
        // Generates a URL-friendly slug from a given phrase
        public static string Slugify(this string phrase)
        {
            var s = phrase.RemoveDiacritics().ToLower();

            // Remove invalid characters except Persian letters and common URL-safe symbols
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]", "");

            // Replace multiple spaces with a single space, then trim
            s = Regex.Replace(s, @"\s+", " ").Trim();

            // Limit length to 45 characters, then trim
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();

            // Replace spaces and @ signs with hyphens
            s = Regex.Replace(s, @"\s", "-");
            s = Regex.Replace(s, @"@", "-");

            return s.ToLower();
        }

        // Removes diacritics from characters — useful for slug creation
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }

}