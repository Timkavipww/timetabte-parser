using System.Text.Encodings.Web;

public static class Handler
{
    public static async Task ProcessTimetableAsync(int index, SemaphoreSlim semaphoreRequest, SemaphoreSlim semaphoreFile)
    {   
        
        await semaphoreRequest.WaitAsync();

        try
        {
            string url = $"https://voenmeh.ru/obrazovanie/timetables/#{Numbers.VALUES[index]}";

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);

                string html = driver.PageSource;
                driver.Quit();
                driver.Dispose();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var dayRows = doc.DocumentNode.SelectNodes("//tr[contains(@class, 'timetable_table_day_row')]");
                var groupTag = doc.GetElementbyId("studsTimetableresult");

                var group = groupTag?.SelectSingleNode(".//p//strong").InnerText.Trim().Substring(26, 6);
                Console.WriteLine($"Группа: {group}");

                string fileName = FileFormatter.SanitizeFileName(group!.ToLower()) + ".json";

                List<DaySchedule> allDays = new List<DaySchedule>();

                if (dayRows == null)
                {
                    Console.WriteLine("Не удалось найти строки с расписанием.");
                    return;
                }

                foreach (var dayRow in dayRows)
                {
                    var daySchedule = TimetableParser.ParseDaySchedule(dayRow);
                    allDays.Add(daySchedule);
                }
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Отключает экранирование Unicode
                };
                string jsonContent = JsonSerializer.Serialize(allDays, options);

                await semaphoreFile.WaitAsync();
                try
                {
                    await File.WriteAllTextAsync(Path.Combine("Schledues", fileName), jsonContent);

                    Console.WriteLine($"Файл с расписанием сохранен как {fileName}");
                }
                finally
                {
                    semaphoreFile.Release();
                }
            }
        }
        finally
        {
            semaphoreRequest.Release();
        }
    }
}