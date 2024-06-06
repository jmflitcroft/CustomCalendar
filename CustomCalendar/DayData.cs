using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar
{
    internal class DayData
    {
        private string m_dayName = "";

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
