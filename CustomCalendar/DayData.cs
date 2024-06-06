using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomCalendar
{
    [DataContract]
    internal class DayData
    {
        [DataMember]
        private string m_dayName = "";

        public string DayName { get { return m_dayName; } }

        public string GetDayName() {  return m_dayName; }

        public void DayNameText_Changed(object? sender, EventArgs e)
        {
            TextBox? dayNameTextBox = sender as TextBox;
            if (dayNameTextBox == null)
            {
                return;
            }

            m_dayName = dayNameTextBox.Text;
        }
    }
}
