public static class TimetableParser
{
    public static DaySchedule ParseDaySchedule(HtmlNode dayRow)
    {
        var dayTextNode = dayRow.SelectSingleNode(".//td//strong");
        string dayText = dayTextNode?.InnerText.Trim() ?? "Неизвестный день";

        var ds = new DaySchedule { Day = dayText };

        var contentRow = dayRow.SelectSingleNode("following-sibling::tr[1]");
        if (contentRow == null) return ds;

        var contentTable = contentRow.SelectSingleNode(".//td//table[contains(@class, 'timetable_table_content')]");
        if (contentTable == null) return ds;

        var tbody = contentTable.SelectSingleNode(".//tbody");
        if (tbody == null) return ds;

        var contentRows = tbody.SelectNodes(".//tr");
        if (contentRows == null) return ds;

        foreach (var tr in contentRows)
        {
            var tdNodes = tr.SelectNodes(".//td");
            if (tdNodes != null)
            {
                var rowData = new List<string>();
                foreach (var td in tdNodes)
                {
                    rowData.Add(td.InnerText.Trim());
                }
                ds.Items.Add(rowData);
            }
        }

        return ds;
    }
}

