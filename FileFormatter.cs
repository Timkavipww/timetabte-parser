public static class FileFormatter
{

    private static readonly Dictionary<char, string> TransliterationMap = new Dictionary<char, string>
    {
        { 'а', "a" }, { 'б', "b" }, { 'в', "v" }, { 'г', "g" }, { 'д', "d" },
        { 'е', "e" }, { 'ё', "yo" }, { 'ж', "zh" }, { 'з', "z" }, { 'и', "i" },
        { 'й', "y" }, { 'к', "k" }, { 'л', "l" }, { 'м', "m" }, { 'н', "n" },
        { 'о', "o" }, { 'п', "p" }, { 'р', "r" }, { 'с', "s" }, { 'т', "t" },
        { 'у', "u" }, { 'ф', "f" }, { 'х', "kh" }, { 'ц', "ts" }, { 'ч', "ch" },
        { 'ш', "sh" }, { 'щ', "shch" }, { 'ы', "y" }, { 'э', "e" }, { 'ю', "yu" },
        { 'я', "ya" },

        { 'А', "A" }, { 'Б', "B" }, { 'В', "V" }, { 'Г', "G" }, { 'Д', "D" },
        { 'Е', "E" }, { 'Ё', "Yo" }, { 'Ж', "Zh" }, { 'З', "Z" }, { 'И', "I" },
        { 'Й', "Y" }, { 'К', "K" }, { 'Л', "L" }, { 'М', "M" }, { 'Н', "N" },
        { 'О', "O" }, { 'П', "P" }, { 'Р', "R" }, { 'С', "S" }, { 'Т', "T" },
        { 'У', "U" }, { 'Ф', "F" }, { 'Х', "Kh" }, { 'Ц', "Ts" }, { 'Ч', "Ch" },
        { 'Ш', "Sh" }, { 'Щ', "Shch" }, { 'Ы', "Y" }, { 'Э', "E" }, { 'Ю', "Yu" },
        { 'Я', "Ya" }
    };

    public static string SanitizeFileName(string input)
    {
        StringBuilder result = new StringBuilder();

        var invalidChars = Path.GetInvalidFileNameChars();

        foreach (char c in input)
        {
            if (TransliterationMap.ContainsKey(c))
            {
                result.Append(TransliterationMap[c]);
            }
            else if (!invalidChars.Contains(c))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}