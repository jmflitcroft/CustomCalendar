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

    [DataContract]
    internal class CalendarData
    {
        [DataMember]
        private List<DayData> m_daysData = new List<DayData>();
        [DataMember]
        private List<MonthData> m_monthsData = new List<MonthData>();

        [DataMember]
        private int m_startingDayComboIndex = 0;
        [DataMember]
        private PageLayoutType m_pageLayoutType = PageLayoutType.Portrait;
        [DataMember]
        private ExportFormatType m_exportFormatType = ExportFormatType.Weekly;

        public List<DayData> GetDaysData() { return m_daysData; }
        public List<MonthData> GetMonthsData() { return m_monthsData; }

        public int GetStartingDayComboIndex() {  return m_startingDayComboIndex; }
        public PageLayoutType GetPageLayoutType() {  return m_pageLayoutType; }
        public ExportFormatType GetExportFormatType() {  return m_exportFormatType; }

        public void SetStartingDayComboIndex(int comboIndex) { m_startingDayComboIndex = comboIndex; }
        public void SetPageLayoutComboIndex(PageLayoutType type) { m_pageLayoutType = type; }
        public void SetExportFormatComboIndex(ExportFormatType type) { m_exportFormatType = type; }
    }
}
