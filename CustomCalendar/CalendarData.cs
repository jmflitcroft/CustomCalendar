using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar
{
    [DataContract(Name = "PageLayoutType")]
    public enum PageLayoutType
    {
        [EnumMember]
        Portrait,
        [EnumMember]
        Landscape
    }

    [DataContract(Name = "ExportFormatType")]
    public enum ExportFormatType
    {
        [EnumMember]
        Weekly,
        [EnumMember]
        Monthly
    }

    struct GeneratedDayData
    {
        public GeneratedDayData()
        {
            m_eventNames = new List<string>();
        }

        public int m_dayNumber = -1;
        public int m_dayInMonthNumber = -1;
        public int m_weekNumber = -1;

        public string m_dayName = "";
        public string m_monthName = "";

        public List<string> m_eventNames;
    }

    [DataContract]
    internal class CalendarData
    {
        [DataMember]
        private List<DayData> m_daysData = new List<DayData>();
        [DataMember]
        private List<MonthData> m_monthsData = new List<MonthData>();
        [DataMember]
        private List<EventData> m_eventsData = new List<EventData>();

        [DataMember]
        private int m_startingDayComboIndex = 0;
        [DataMember]
        private PageLayoutType m_pageLayoutType = PageLayoutType.Portrait;
        [DataMember]
        private ExportFormatType m_exportFormatType = ExportFormatType.Weekly;

        public List<DayData> GetDaysData() { return m_daysData; }
        public List<MonthData> GetMonthsData() { return m_monthsData; }
        public List<EventData> GetEventsData() { return m_eventsData; }

        public int GetStartingDayComboIndex() { return m_startingDayComboIndex; }
        public PageLayoutType GetPageLayoutType() { return m_pageLayoutType; }
        public ExportFormatType GetExportFormatType() { return m_exportFormatType; }

        public void SetStartingDayComboIndex(int comboIndex) { m_startingDayComboIndex = comboIndex; }
        public void SetPageLayoutComboIndex(PageLayoutType type) { m_pageLayoutType = type; }
        public void SetExportFormatComboIndex(ExportFormatType type) { m_exportFormatType = type; }

        public void OnLoaded()
        {
            // Make sure we have valid values after loading.
            if (m_daysData == null)
            {
                m_daysData = new List<DayData>();
            }
            if (m_monthsData == null)
            {
                m_monthsData = new List<MonthData>();
            }
            if (m_eventsData == null)
            {
                m_eventsData = new List<EventData>();
            }
        }

        public GeneratedDayData[] GenerateDaysInYear(int startingDayIndex)
        {
            int daysInYear = 0;
            foreach (MonthData monthData in m_monthsData)
            {
                daysInYear += monthData.GetNumberOfDays();
            }

            if (daysInYear == 0)
            {
                return [];
            }

            int dayInMonthNumber = 1;
            int monthNumber = 0;
            MonthData currentMonthData = m_monthsData[monthNumber];
            GeneratedDayData[] generatedDayDatas = new GeneratedDayData[daysInYear];

            foreach (EventData eventData in m_eventsData)
            {
                eventData.m_workingEventLength = 0;
                eventData.m_workingEventFrequency = -1;

                if (eventData.GetRepeatFrequency() > 0 && eventData.GetMonthName() == "")
                {
                    eventData.m_workingEventFrequency = 1;
                }
            }

            for (int i = 0; i < daysInYear; ++i)
            {
                DayData currentDay = m_daysData[(startingDayIndex + i) % m_daysData.Count];

                ref GeneratedDayData generatedDayData = ref generatedDayDatas[i];
                generatedDayData.m_dayNumber = i;
                generatedDayData.m_dayInMonthNumber = dayInMonthNumber;
                generatedDayData.m_dayName = currentDay.DayName;
                generatedDayData.m_monthName = currentMonthData.MonthName;
                generatedDayData.m_weekNumber = (i + startingDayIndex) / m_daysData.Count;
                generatedDayData.m_eventNames = new List<string>();

                foreach (EventData eventData in m_eventsData)
                {
                    if (eventData.m_workingEventFrequency > 0)
                    {
                        --eventData.m_workingEventFrequency;
                    }

                    if ((generatedDayData.m_monthName == eventData.GetMonthName() &&
                        generatedDayData.m_dayInMonthNumber == eventData.GetDayIndex()) ||
                        eventData.m_workingEventFrequency == 0)
                    {
                        eventData.m_workingEventLength = 1;
                        eventData.m_workingEventFrequency = -1;

                        generatedDayData.m_eventNames.Add(eventData.GetEventNameDisplay());
                    }
                    else if (eventData.m_workingEventLength > 0 && eventData.m_workingEventLength < eventData.GetEventLength())
                    {
                        ++eventData.m_workingEventLength;
                        generatedDayData.m_eventNames.Add(eventData.GetEventNameDisplay());
                    }

                    if (eventData.m_workingEventLength > 0 && eventData.m_workingEventLength == eventData.GetEventLength())
                    {
                        eventData.m_workingEventLength = 0;
                        if (eventData.GetRepeatFrequency() > 0)
                        {
                            eventData.m_workingEventFrequency = eventData.GetRepeatFrequency();
                        }
                    }
                }

                ++dayInMonthNumber;
                if (dayInMonthNumber > currentMonthData.GetNumberOfDays())
                {
                    ++monthNumber;
                    dayInMonthNumber = 1;

                    if (monthNumber < m_monthsData.Count)
                    {
                        currentMonthData = m_monthsData[monthNumber];
                    }
                }
            }

            return generatedDayDatas;
        }

        public GeneratedDayData[][] GenerateDaysInYearByMonth(int startingDayIndex)
        {
            GeneratedDayData[] generatedDays = GenerateDaysInYear(startingDayIndex);
            GeneratedDayData[][] generatedDaysByMonth = new GeneratedDayData[m_monthsData.Count][];

            int generatedDaysIndex = 0;
            for (int i = 0; i < m_monthsData.Count; ++i)
            {
                MonthData currentMonth = m_monthsData[i];
                generatedDaysByMonth[i] = new GeneratedDayData[currentMonth.GetNumberOfDays()];
                for (int j = 0; j < currentMonth.GetNumberOfDays(); ++j)
                {
                    generatedDaysByMonth[i][j] = generatedDays[generatedDaysIndex++];
                }
            }

            return generatedDaysByMonth;
        }

        public List<GeneratedDayData[]> GenerateDaysInYearByWeek(int startingDayIndex)
        {
            GeneratedDayData[] generatedDays = GenerateDaysInYear(startingDayIndex);

            if (generatedDays.Length == 0)
            {
                return new List<GeneratedDayData[]>();
            }

            List<GeneratedDayData[]> generatedDaysByWeek = new List<GeneratedDayData[]>();
            List<GeneratedDayData> generatedWeekData = new List<GeneratedDayData>();

            int currentWeekNumber = generatedDays[0].m_weekNumber;
            string currentMonthName = generatedDays[0].m_monthName;
            for (int i = 0; i < generatedDays.Length; ++i)
            {
                GeneratedDayData dayData = generatedDays[i];
                if (dayData.m_monthName != currentMonthName ||
                    dayData.m_weekNumber != currentWeekNumber)
                {
                    generatedDaysByWeek.Add(generatedWeekData.ToArray());
                    generatedWeekData.Clear();

                    currentWeekNumber = dayData.m_weekNumber;
                    currentMonthName = dayData.m_monthName;
                }

                generatedWeekData.Add(dayData);
            }

            generatedDaysByWeek.Add(generatedWeekData.ToArray());

            return generatedDaysByWeek;
        }
    }
}