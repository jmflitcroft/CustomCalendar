using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar
{
    [DataContract]
    internal class MonthData
    {
        [DataMember]
        private string m_monthName = "";
        [DataMember]
        private int m_numberOfDays = 30;

        public string MonthName { get { return m_monthName; } }

        public string GetMonthName() { return m_monthName; }
        public int GetNumberOfDays() {  return m_numberOfDays; }


        public void MonthNameText_Changed(object? sender, EventArgs e)
        {
            TextBox? monthNameTextBox = sender as TextBox;
            if (monthNameTextBox == null)
            {
                return;
            }
            m_monthName = monthNameTextBox.Text;
        }

        public void NumberOfDaysText_Changed(object? sender, EventArgs e)
        {
            TextBox? numberOfDaysTextBox = sender as TextBox;
            if (numberOfDaysTextBox == null)
            {
                return;
            }

            Int32.TryParse(numberOfDaysTextBox.Text, out m_numberOfDays);
        }
    }
}
