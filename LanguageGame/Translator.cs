using System;
using System.Globalization;
using System.Text;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay".
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                throw new ArgumentException($"String is null or empty{nameof(phrase)}");
            }

            StringBuilder result = new StringBuilder();
            StringBuilder word = new StringBuilder();
            for (int i = 0; i < phrase.Length; i++)
            {
                if (char.IsLetter(phrase[i]) || phrase[i] == '’')
                {
                    word.Append(phrase[i]);
                }
                else
                {
                    if (!string.IsNullOrEmpty(word.ToString()))
                    {
                        result.Append(TranslatorWord(word.ToString()));
                        word.Clear();
                    }

                    result.Append(phrase[i]);
                }
            }

            return string.IsNullOrEmpty(result.ToString()) ? TranslatorWord(phrase) : result.ToString();
        }

        private static string TranslatorWord(string word)
        {
            string result = default;
            string vowel = "aeiou";

            if (vowel.ToUpper(CultureInfo.CurrentCulture).IndexOf(word[0], StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                result = word + "yay";
            }
            else
            {
                int count = 0;
                StringBuilder end = new StringBuilder();
                foreach (char c in word)
                {
                    if (!vowel.Contains(c, StringComparison.CurrentCulture))
                    {
                        end.Append(c);
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                result = word[count..] + end + "ay";
                if (char.IsUpper(word[0]))
                {
                    result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower(CultureInfo.CurrentCulture));
                }
            }

            return result;
        }
    }
}
