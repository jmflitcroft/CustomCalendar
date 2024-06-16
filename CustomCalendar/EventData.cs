using CustomCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar
{
    [DataContract]
    internal class EventData
    {
        public delegate List<MonthData> GetMonthsData();

        [DataMember]
        private string m_eventName = "";
        [DataMember]
        private int m_eventLength = 1;
        [DataMember]
        private string m_monthName = "";
        [DataMember]
        private int m_dayIndex = 1;
        [DataMember]
        private int m_repeatFrequency = 0;

        [IgnoreDataMember, NonSerialized]
        private GetMonthsData? m_getMonthDataCallback = null;
        [IgnoreDataMember, NonSerialized]
        private NumericUpDown? m_dayIndexUpDown = null;

        public int m_workingEventLength = 0;
        public int m_workingEventFrequency = -1;

        public string GetEventName() { return m_eventName; }
        public int GetEventLength() { return m_eventLength; }
        public string GetMonthName() { return m_monthName; }
        public int GetDayIndex() { return m_dayIndex; }
        public int GetRepeatFrequency() { return m_repeatFrequency; }
        public void SetMonthDataCallback(GetMonthsData newCallback) { m_getMonthDataCallback = newCallback; }

        public string GetEventNameDisplay()
        {
            string eventName = m_eventName;

            if (m_eventLength > 1)
            {
                eventName += " (" + m_workingEventLength + "/" + m_eventLength + ")";
            }

            return eventName;
        }

        public void SetDayIndexUpDown(NumericUpDown upDown)
        {
            m_dayIndexUpDown = upDown;
        }

        public void UpdateMonthComboBox(ComboBox monthNameComboBox)
        {
            int selectedIndex = -1;
            monthNameComboBox.Items.Clear();

            if (m_getMonthDataCallback != null)
            {
                foreach (MonthData monthData in m_getMonthDataCallback())
                {
                    monthNameComboBox.Items.Add(monthData.GetMonthName());

                    if (monthData.GetMonthName() == m_monthName)
                    {
                        selectedIndex = monthNameComboBox.Items.Count - 1;
                    }
                }
            }

            monthNameComboBox.SelectedIndex = selectedIndex;
        }

        public void UpdateDayIndexUpDownMax()
        {
            if (m_dayIndexUpDown != null)
            {
                List<MonthData> monthsData = m_getMonthDataCallback != null ? m_getMonthDataCallback() : new List<MonthData>();
                for (int i = 0; i < monthsData.Count; ++i)
                {
                    MonthData monthData = monthsData[i];
                    if (m_monthName == monthData.GetMonthName())
                    {
                        m_dayIndexUpDown.Maximum = monthData.GetNumberOfDays();
                        break;
                    }
                }
            }
        }

        public void EventNameText_Changed(object? sender, EventArgs e)
        {
            TextBox? eventNameTextBox = sender as TextBox;
            if (eventNameTextBox == null)
            {
                return;
            }
            m_eventName = eventNameTextBox.Text;
        }

        public void EventLength_Changed(object? sender, EventArgs e)
        {
            NumericUpDown? eventLengthUpDown = sender as NumericUpDown;
            if (eventLengthUpDown == null)
            {
                return;
            }

            m_eventLength = (int)eventLengthUpDown.Value;
        }

        public void DayIndexUpDown_ValueChanged(object? sender, EventArgs e)
        {
            NumericUpDown? dayIndexUpDown = sender as NumericUpDown;
            if (dayIndexUpDown == null)
            {
                return;
            }

            m_dayIndex = (int)dayIndexUpDown.Value;
        }

        public void MonthNameComboBox_SelectedValueChanged(object? sender, EventArgs e)
        {
            ComboBox? monthNameComboBox = sender as ComboBox;
            if (monthNameComboBox == null)
            {
                return;
            }

            string? selectedItem = monthNameComboBox.SelectedItem as string;
            if (selectedItem == null)
            {
                return;
            }

            m_monthName = selectedItem;
            UpdateDayIndexUpDownMax();
        }

        public void MonthNameComboBox_MouseClick(object? sender, MouseEventArgs e)
        {
            ComboBox? monthNameComboBox = sender as ComboBox;
            if (monthNameComboBox == null)
            {
                return;
            }

            UpdateMonthComboBox(monthNameComboBox);
        }

        public void RepeatFrequency_Changed(object? sender, EventArgs e)
        {
            NumericUpDown? repeatUpDown = sender as NumericUpDown;
            if (repeatUpDown == null)
            {
                return;
            }

            m_repeatFrequency = (int)repeatUpDown.Value;
        }
    }
}