using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace CustomCalendar
{
    public partial class Form1 : Form
    {
        CalendarData m_calendarData = new CalendarData();

        public Form1()
        {
            InitializeComponent();

            pageLayoutComboBox.Items.Add(PageLayoutType.Portrait);
            pageLayoutComboBox.Items.Add(PageLayoutType.Landscape);
            pageLayoutComboBox.SelectedIndex = 0;

            exportFormatComboBox.Items.Add(ExportFormatType.Weekly);
            exportFormatComboBox.Items.Add(ExportFormatType.Monthly);
            exportFormatComboBox.SelectedIndex = 0;
        }

        private void addDayUI(DayData dayData)
        {
            // Background
            Panel newPanel = new Panel();
            newPanel.BorderStyle = BorderStyle.FixedSingle;
            Size backgroundSize = new Size(allDaysFlowPanel.Width - 8, 0);

            // Day Name
            Label enterDayNameLabel = new Label();
            enterDayNameLabel.Location = new System.Drawing.Point(0, 5);
            enterDayNameLabel.Text = "Day Name: ";
            enterDayNameLabel.AutoSize = true;
            enterDayNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            newPanel.Controls.Add(enterDayNameLabel);

            TextBox dayNameTextBox = new TextBox();
            dayNameTextBox.Text = dayData.GetDayName();
            dayNameTextBox.Location = new System.Drawing.Point(enterDayNameLabel.Size.Width, 5);
            dayNameTextBox.Size = new Size(backgroundSize.Width - dayNameTextBox.Location.X - 10, dayNameTextBox.Size.Height);
            dayNameTextBox.TextChanged += new EventHandler(dayData.DayNameText_Changed);
            newPanel.Controls.Add(dayNameTextBox);
            enterDayNameLabel.Size = new Size(enterDayNameLabel.Width, dayNameTextBox.Size.Height);

            // Remove Day
            Button removeDayButton = new Button();
            removeDayButton.Text = "Remove";
            removeDayButton.Location = new System.Drawing.Point(0, dayNameTextBox.Bottom + 10);
            removeDayButton.Click += new EventHandler(removeDayButton_Clicked);
            newPanel.Controls.Add(removeDayButton);

            backgroundSize.Height = removeDayButton.Bottom + 10;
            newPanel.Size = backgroundSize;
            allDaysFlowPanel.Controls.Add(newPanel);
        }

        private void addDayButton_Click(object sender, EventArgs e)
        {
            DayData newDayData = new DayData();
            m_calendarData.GetDaysData().Add(newDayData);

            addDayUI(newDayData);
        }

        private void addMonthUI(MonthData monthData)
        {
            Panel newPanel = new Panel();
            newPanel.BorderStyle = BorderStyle.FixedSingle;
            Size backgroundSize = new Size(allMonthsFlowPanel.Width - 8, 0);

            // Month Name
            Label monthNameLabel = new Label();
            monthNameLabel.Text = "Month Name: ";
            monthNameLabel.AutoSize = true;
            newPanel.Controls.Add(monthNameLabel);

            TextBox monthNameTextBox = new TextBox();
            monthNameTextBox.Text = monthData.GetMonthName();
            monthNameTextBox.TextChanged += new EventHandler(monthData.MonthNameText_Changed);
            newPanel.Controls.Add(monthNameTextBox);

            // Number of Days
            Label numberOfDaysLabel = new Label();
            numberOfDaysLabel.Text = "Number of Days: ";
            numberOfDaysLabel.AutoSize = true;
            numberOfDaysLabel.Location = new System.Drawing.Point(0, monthNameTextBox.Bottom);
            newPanel.Controls.Add(numberOfDaysLabel);

            TextBox numberOfDaysTextBox = new TextBox();
            numberOfDaysTextBox.Text = "" + monthData.GetNumberOfDays();
            numberOfDaysTextBox.TextChanged += new EventHandler(monthData.NumberOfDaysText_Changed);
            newPanel.Controls.Add(numberOfDaysTextBox);

            // Update Text Box Position & Size.
            int textBoxXPosition = Math.Max(monthNameLabel.Size.Width, numberOfDaysLabel.Size.Width);
            int textBoxWidth = backgroundSize.Width - textBoxXPosition - 10;
            monthNameTextBox.Location = new System.Drawing.Point(textBoxXPosition, 0);
            monthNameTextBox.Size = new Size(textBoxWidth, monthNameTextBox.Size.Height);
            numberOfDaysTextBox.Location = new System.Drawing.Point(textBoxXPosition, monthNameTextBox.Bottom);
            numberOfDaysTextBox.Size = new Size(textBoxWidth, numberOfDaysTextBox.Size.Height);


            // Remove Month
            Button removeMonthButton = new Button();
            removeMonthButton.Text = "Remove";
            removeMonthButton.Location = new System.Drawing.Point(0, numberOfDaysTextBox.Bottom);
            removeMonthButton.Click += new EventHandler(removeMonthButton_Clicked);
            newPanel.Controls.Add(removeMonthButton);


            backgroundSize.Height = removeMonthButton.Bottom + 10;
            newPanel.Size = backgroundSize;
            newPanel.Size = backgroundSize;
            allMonthsFlowPanel.Controls.Add(newPanel);
        }

        private void addMonthButton_Click(object sender, EventArgs e)
        {
            MonthData newMonthData = new MonthData();
            m_calendarData.GetMonthsData().Add(newMonthData);

            addMonthUI(newMonthData);
        }

        private void removeDayButton_Clicked(object? sender, EventArgs e)
        {
            Button? removeDayButton = sender as Button;
            if (removeDayButton == null)
            {
                return;
            }

            int panelIndex = allDaysFlowPanel.Controls.IndexOf(removeDayButton.Parent);
            if (panelIndex != -1)
            {
                m_calendarData.GetDaysData().RemoveAt(panelIndex);
                allDaysFlowPanel.Controls.RemoveAt(panelIndex);
            }
        }

        private void removeMonthButton_Clicked(object? sender, EventArgs e)
        {
            Button? removeMonthButton = sender as Button;
            if (removeMonthButton == null)
            {
                return;
            }

            int panelIndex = allMonthsFlowPanel.Controls.IndexOf(removeMonthButton.Parent);
            if (panelIndex != -1)
            {
                m_calendarData.GetMonthsData().RemoveAt(panelIndex);
                allMonthsFlowPanel.Controls.RemoveAt(panelIndex);
            }
        }

        private void generatePDFButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PDF document (*.pdf)|*.pdf";
            //saveFileDialog.FilterIndex = 2;
            //saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    PdfWriter pdfWriter = new PdfWriter(myStream);
                    PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                    Document document = new Document(pdfDocument, getPageSize());
                    document.SetMargins(20, 20, 20, 20);


                    if (m_calendarData.GetExportFormatType() == ExportFormatType.Monthly)
                    {
                        GeneratedDayData[][] generatedDaysByMonth = m_calendarData.GenerateDaysInYearByMonth(m_calendarData.GetStartingDayComboIndex());
                        for (int i = 0; i < generatedDaysByMonth.Length; ++i)
                        {
                            GeneratedDayData[] monthData = generatedDaysByMonth[i];
                            if (monthData.Length == 0)
                            {
                                continue;
                            }

                            createMonthPDFTable(document, monthData);

                            if (i < generatedDaysByMonth.Length - 1)
                            {
                                document.Add(new AreaBreak());
                            }
                        }
                    }
                    else
                    {
                        List<GeneratedDayData[]> generatedDaysByWeek = m_calendarData.GenerateDaysInYearByWeek(m_calendarData.GetStartingDayComboIndex());
                        for (int i = 0; i < generatedDaysByWeek.Count; ++i)
                        {
                            GeneratedDayData[] weekData = generatedDaysByWeek[i];
                            if (weekData.Length == 0)
                            {
                                continue;
                            }

                            createWeeklyPDFTable(document, weekData);

                            if (i < generatedDaysByWeek.Count - 1)
                            {
                                document.Add(new AreaBreak());
                            }
                        }
                    }

                    //GeneratedDayData[] generatedDays = m_calendarData.GenerateDaysInYear(m_calendarData.GetStartingDayComboIndex());


                    //int monthDayStartingIndex = m_calendarData.GetStartingDayComboIndex();
                    //int weekNumber = 0;

                    //for (int i = 0; i < m_calendarData.GetMonthsData().Count; ++i)
                    //{
                    //    MonthData monthData = m_calendarData.GetMonthsData()[i];

                    //    if (m_calendarData.GetExportFormatType() == ExportFormatType.Monthly)
                    //    {
                    //        //createMonthPDFTable(document, monthData, monthDayStartingIndex);

                    //        //monthDayStartingIndex = (monthDayStartingIndex + monthData.GetNumberOfDays()) % m_calendarData.GetDaysData().Count;

                    //        //if (i < m_calendarData.GetMonthsData().Count - 1)
                    //        //{
                    //        //    document.Add(new AreaBreak());
                    //        //}
                    //    }
                    //    else
                    //    {
                    //        int numberOfWeeksInMonth = (int)Math.Ceiling((float)monthData.GetNumberOfDays() / (float)m_calendarData.GetDaysData().Count);
                    //        int dayNumber = -monthDayStartingIndex;
                    //        int startingDayInMonth = monthDayStartingIndex;
                    //        while (dayNumber < monthData.GetNumberOfDays())
                    //        {
                    //            if (weekNumber > 0)
                    //            {
                    //                document.Add(new AreaBreak());
                    //            }

                    //            createWeeklyPDFTable(document, monthData, weekNumber++, dayNumber, startingDayInMonth);

                    //            dayNumber += m_calendarData.GetDaysData().Count;

                    //        }
                    //        for (int j = 0; j < numberOfWeeksInMonth; ++j)
                    //        {

                    //        }

                    //        monthDayStartingIndex = (monthDayStartingIndex + monthData.GetNumberOfDays()) % m_calendarData.GetDaysData().Count;
                    //    }
                    //}

                    pdfDocument.Close();
                    myStream.Close();
                }
            }
        }

        private void createWeeklyPDFTable(Document document, GeneratedDayData[] daysInWeek)
        {
            Table header = new Table(UnitValue.CreatePercentArray(3));
            header.SetWidth(UnitValue.CreatePercentValue(100));
            header.AddCell(new Cell().Add(new Paragraph("Week: " + (daysInWeek[0].m_weekNumber + 1))));
            header.AddCell(new Cell().Add(new Paragraph("Month: " + daysInWeek[0].m_monthName)));
            header.AddCell(new Cell().Add(new Paragraph("Year: ")));
            header.SetPaddingBottom(10);
            header.SetHeight(UnitValue.CreatePointValue(30));
            document.Add(header);

            UnitValue headerHeight = header.GetHeight();

            var table = new Table(UnitValue.CreatePercentArray(m_calendarData.GetDaysData().Count));
            table.SetWidth(UnitValue.CreatePercentValue(100));

            table.SetHeight(getPageSize().GetHeight() - header.GetHeight().GetValue());
            foreach (DayData day in m_calendarData.GetDaysData())
            {
                table.AddHeaderCell(day.GetDayName());
            }

            int startingDayInWeekNumber = m_calendarData.GetDaysData().FindIndex(x => x.DayName == daysInWeek[0].m_dayName);
            int endingDayInWeekNumber = m_calendarData.GetDaysData().FindIndex(x => x.DayName == daysInWeek[daysInWeek.Length - 1].m_dayName);
            for (int i = 0; i < startingDayInWeekNumber; ++i)
            {
                Cell cell = new Cell();
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
                table.AddCell(cell);
            }

            for (int i = 0; i < daysInWeek.Length; ++i)
            {
                GeneratedDayData dayData = daysInWeek[i];

                Cell cell = new Cell();
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.WHITE);
                cell.Add(new Paragraph("" + (dayData.m_dayInMonthNumber)));

                foreach (string eventName in dayData.m_eventNames)
                {
                    cell.Add(new Paragraph(eventName));
                }

                table.AddCell(cell);
            }

            for (int i = endingDayInWeekNumber + 1; i < m_calendarData.GetDaysData().Count; ++i)
            {
                Cell cell = new Cell();
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
                table.AddCell(cell);
            }

            document.Add(table);
        }

        private void createMonthPDFTable(Document document, GeneratedDayData[] daysInMonth)
        {
            Table header = new Table(UnitValue.CreatePercentArray(2));
            header.SetWidth(UnitValue.CreatePercentValue(100));
            header.AddCell(new Cell().Add(new Paragraph("Month: " + daysInMonth[0].m_monthName)));
            header.AddCell(new Cell().Add(new Paragraph("Year: " + 0)));
            header.SetPaddingBottom(10);
            header.SetHeight(UnitValue.CreatePointValue(30));
            document.Add(header);

            UnitValue headerHeight = header.GetHeight();

            var table = new Table(UnitValue.CreatePercentArray(m_calendarData.GetDaysData().Count));
            table.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (DayData day in m_calendarData.GetDaysData())
            {
                table.AddHeaderCell(day.GetDayName());
            }

            table.SetHeight(getPageSize().GetHeight() - header.GetHeight().GetValue());

            int dayInWeekNumber = m_calendarData.GetDaysData().FindIndex(x => x.DayName == daysInMonth[0].m_dayName);
            for (int i = 0; i < dayInWeekNumber; ++i)
            {
                Cell cell = new Cell();
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
                table.AddCell(cell);
            }

            for (int i = 0; i < daysInMonth.Length; ++i)
            {
                GeneratedDayData dayData = daysInMonth[i];

                Cell cell = new Cell();
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.WHITE);
                cell.Add(new Paragraph("" + (dayData.m_dayInMonthNumber)));

                foreach (string eventName in dayData.m_eventNames)
                {
                    cell.Add(new Paragraph(eventName));
                }

                table.AddCell(cell);

            }

            document.Add(table);
        }

        private PageSize getPageSize()
        {
            if (pageLayoutComboBox.SelectedIndex == 0)
            {
                return PageSize.A4;
            }
            else
            {
                return PageSize.A4.Rotate();
            }
        }

        private void tabControl_Selected(object sender, EventArgs e)
        {
            TabControl? tabControl = sender as TabControl;
            if (tabControl == null)
            {
                return;
            }

            if (tabControl.TabPages[tabControl.SelectedIndex] == finalizeTabPage)
            {
                int prevSelectedIndex = startingDayComboBox.SelectedIndex;
                startingDayComboBox.Items.Clear();
                foreach (DayData day in m_calendarData.GetDaysData())
                {
                    startingDayComboBox.Items.Add(day.GetDayName());
                }

                if (startingDayComboBox.Items.Count == 0)
                {
                    startingDayComboBox.SelectedIndex = -1;
                    startingDayComboBox.ResetText();
                }
                else
                {
                    startingDayComboBox.SelectedIndex = Math.Clamp(prevSelectedIndex, 0, startingDayComboBox.Items.Count - 1);
                }
            }
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "xml files (*.xml) | *.xml";
            //saveFileDialog.FilterIndex = 2;
            //saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                myStream = saveFileDialog.OpenFile();

                if (myStream != null)
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarData));
                    serializer.WriteObject(myStream, m_calendarData);

                    myStream.Close();

                    //string jsonData = JsonSerializer.Serialize(m_calendarData);

                    //UTF8Encoding utf8Encoding = new UTF8Encoding();
                    //myStream.Write(utf8Encoding.GetBytes(jsonData));
                    //myStream.Write(jsonData.)

                    //myStream.Write(jsonData);

                    //MemoryStream ms = new MemoryStream();

                    //BinaryFormatter formatter = new BinaryFormatter();
                    //formatter.Serialize(ms, m_calendarData);

                    //// Your employees object serialised and converted to a string.
                    //string encodedObject = Convert.ToBase64String(ms.ToArray());

                    //ms.Close();



                    //PdfWriter pdfWriter = new PdfWriter(myStream);
                    //PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                    //Document document = new Document(pdfDocument, getPageSize());
                    //document.SetMargins(20, 20, 20, 20);

                    //if (exportFormatComboBox.SelectedIndex == 0)
                    //{
                    //    createWeeklyPDFTable(document);
                    //}
                    //else
                    //{
                    //    int monthDayStartingIndex = startingDayComboBox.SelectedIndex >= 0 ? startingDayComboBox.SelectedIndex : 0;

                    //    for (int i = 0; i < m_calendarData.GetMonthsData().Count; ++i)
                    //    {
                    //        MonthData monthData = m_calendarData.GetMonthsData()[i];
                    //        createMonthPDFTable(document, monthData, monthDayStartingIndex);

                    //        monthDayStartingIndex = (monthDayStartingIndex + monthData.GetNumberOfDays()) % m_calendarData.GetDaysData().Count;

                    //        if (i < m_calendarData.GetMonthsData().Count - 1)
                    //        {
                    //            document.Add(new AreaBreak());
                    //        }
                    //    }
                    //}

                    //pdfDocument.Close();

                }
            }
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "xml files (*.xml) | *.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                myStream = openFileDialog.OpenFile();
                if (myStream != null)
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(CalendarData));
                    CalendarData? newCalendarData = serializer.ReadObject(myStream) as CalendarData;
                    if (newCalendarData != null)
                    {
                        m_calendarData = newCalendarData;
                        m_calendarData.OnLoaded();

                        allDaysFlowPanel.Controls.Clear();
                        foreach (DayData dayData in m_calendarData.GetDaysData())
                        {
                            addDayUI(dayData);
                        }

                        allMonthsFlowPanel.Controls.Clear();
                        foreach (MonthData monthData in m_calendarData.GetMonthsData())
                        {
                            addMonthUI(monthData);
                        }

                        allEventsPanel.Controls.Clear();
                        for (int i = 0; i < m_calendarData.GetEventsData().Count; ++i)
                        {
                            EventData fixedEventData = m_calendarData.GetEventsData()[i];
                            fixedEventData.SetMonthDataCallback(m_calendarData.GetMonthsData);
                            addFixedEventUI(fixedEventData);
                        }

                        startingDayComboBox.Items.Clear();
                        foreach (DayData day in m_calendarData.GetDaysData())
                        {
                            startingDayComboBox.Items.Add(day.GetDayName());
                        }
                        startingDayComboBox.SelectedIndex = m_calendarData.GetStartingDayComboIndex();

                        pageLayoutComboBox.SelectedItem = m_calendarData.GetPageLayoutType();
                        exportFormatComboBox.SelectedItem = m_calendarData.GetExportFormatType();

                    }

                    myStream.Close();
                }
            }
        }


        private void startingDayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            if (comboBox == null)
            {
                return;
            }

            m_calendarData.SetStartingDayComboIndex(comboBox.SelectedIndex);
        }

        private void pageLayoutComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            object? selectedValue = comboBox != null ? comboBox.SelectedItem : null;
            if (comboBox == null || selectedValue == null)
            {
                return;
            }

            m_calendarData.SetPageLayoutComboIndex((PageLayoutType)selectedValue);
        }

        private void exportFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            object? selectedValue = comboBox != null ? comboBox.SelectedItem : null;
            if (comboBox == null || selectedValue == null)
            {
                return;
            }

            m_calendarData.SetExportFormatComboIndex((ExportFormatType)selectedValue);
        }



        private void addFixedEventUI(EventData eventData)
        {
            Panel newPanel = new Panel();
            newPanel.BorderStyle = BorderStyle.FixedSingle;
            Size backgroundSize = new Size(allEventsPanel.Width - 8, 0);

            // Event Name
            Label eventNameLabel = new Label();
            eventNameLabel.Text = "Event Name: ";
            eventNameLabel.AutoSize = true;
            newPanel.Controls.Add(eventNameLabel);

            TextBox eventNameTextBox = new TextBox();
            eventNameTextBox.Location = new System.Drawing.Point(eventNameLabel.Size.Width, 0);
            eventNameTextBox.Text = eventData.GetEventName();
            eventNameTextBox.TextChanged += eventData.EventNameText_Changed;
            newPanel.Controls.Add(eventNameTextBox);

            // Event Length
            Label eventLengthLabel = new Label();
            eventLengthLabel.Location = new System.Drawing.Point(0, eventNameTextBox.Bottom);
            eventLengthLabel.Text = "Event Length: ";
            eventLengthLabel.AutoSize = true;
            newPanel.Controls.Add(eventLengthLabel);

            NumericUpDown eventLengthUpDown = new NumericUpDown();
            eventLengthUpDown.Location = new System.Drawing.Point(eventLengthLabel.Size.Width, eventLengthLabel.Location.Y);
            eventLengthUpDown.Value = eventData.GetEventLength();
            eventLengthUpDown.DecimalPlaces = 0;
            eventLengthUpDown.ValueChanged += eventData.EventLength_Changed;
            newPanel.Controls.Add(eventLengthUpDown);

            // Month Name
            Label monthNameLabel = new Label();
            monthNameLabel.Location = new System.Drawing.Point(0, eventLengthUpDown.Bottom);
            monthNameLabel.Text = "Month Name: ";
            monthNameLabel.AutoSize = true;
            newPanel.Controls.Add(monthNameLabel);

            ComboBox monthNameComboBox = new ComboBox();
            monthNameComboBox.Location = new System.Drawing.Point(monthNameLabel.Size.Width, monthNameLabel.Location.Y);
            monthNameComboBox.MouseClick += eventData.MonthNameComboBox_MouseClick;
            monthNameComboBox.SelectedValueChanged += eventData.MonthNameComboBox_SelectedValueChanged;
            eventData.UpdateMonthComboBox(monthNameComboBox);
            newPanel.Controls.Add(monthNameComboBox);

            // Day Index
            Label dayIndexLabel = new Label();
            dayIndexLabel.Location = new System.Drawing.Point(monthNameComboBox.Right + 20, monthNameLabel.Location.Y);
            dayIndexLabel.Text = "Day Number: ";
            dayIndexLabel.AutoSize = true;
            newPanel.Controls.Add(dayIndexLabel);

            NumericUpDown dayIndexUpDown = new NumericUpDown();
            dayIndexUpDown.Location = new System.Drawing.Point(dayIndexLabel.Right, dayIndexLabel.Location.Y);
            dayIndexUpDown.DecimalPlaces = 0;
            dayIndexUpDown.Minimum = 1;
            dayIndexUpDown.Maximum = 1;
            dayIndexUpDown.ValueChanged += eventData.DayIndexUpDown_ValueChanged;
            newPanel.Controls.Add(dayIndexUpDown);
            eventData.SetDayIndexUpDown(dayIndexUpDown);
            eventData.UpdateDayIndexUpDownMax();
            dayIndexUpDown.Value = eventData.GetDayIndex();

            // Repeat Frequency
            Label repeatFrequencyLabel = new Label();
            repeatFrequencyLabel.Location = new System.Drawing.Point(0, dayIndexUpDown.Bottom);
            repeatFrequencyLabel.Text = "Occurs Every X Days: ";
            repeatFrequencyLabel.AutoSize = true;
            newPanel.Controls.Add(repeatFrequencyLabel);

            NumericUpDown repeatFrequencyUpDown = new NumericUpDown();
            repeatFrequencyUpDown.Location = new System.Drawing.Point(repeatFrequencyLabel.Size.Width, repeatFrequencyLabel.Location.Y);
            repeatFrequencyUpDown.Value = eventData.GetRepeatFrequency();
            repeatFrequencyUpDown.DecimalPlaces = 0;
            repeatFrequencyUpDown.ValueChanged += eventData.RepeatFrequency_Changed;
            newPanel.Controls.Add(repeatFrequencyUpDown);

            // Update Positions & Size.
            int longestLabelWidth = Math.Max(eventNameLabel.Size.Width, Math.Max(eventLengthLabel.Size.Width, Math.Max(monthNameLabel.Size.Width, Math.Max(repeatFrequencyLabel.Size.Width, dayIndexLabel.Size.Width))));
            int textBoxWidth = backgroundSize.Width - longestLabelWidth - 10;
            eventNameTextBox.Location = new System.Drawing.Point(longestLabelWidth, 0);
            eventNameTextBox.Size = new Size(textBoxWidth, eventNameLabel.Size.Height);
            eventLengthUpDown.Location = new System.Drawing.Point(longestLabelWidth, eventLengthUpDown.Location.Y);
            monthNameComboBox.Location = new System.Drawing.Point(longestLabelWidth, monthNameComboBox.Location.Y);
            dayIndexLabel.Location = new System.Drawing.Point(monthNameComboBox.Right + 20, monthNameLabel.Location.Y);
            dayIndexUpDown.Location = new System.Drawing.Point(dayIndexLabel.Right, dayIndexLabel.Location.Y);
            repeatFrequencyUpDown.Location = new System.Drawing.Point(longestLabelWidth, repeatFrequencyUpDown.Location.Y);
            
            // Remove Event
            Button removeEventButton = new Button();
            removeEventButton.Text = "Remove";
            removeEventButton.Location = new System.Drawing.Point(0, repeatFrequencyUpDown.Bottom);
            removeEventButton.Click += removeEventButton_Clicked;
            newPanel.Controls.Add(removeEventButton);


            backgroundSize.Height = removeEventButton.Bottom + 10;
            newPanel.Size = backgroundSize;
            allEventsPanel.Controls.Add(newPanel);
        }

        private void removeEventButton_Clicked(object? sender, EventArgs e)
        {
            Button? removeEventButton = sender as Button;
            if (removeEventButton == null)
            {
                return;
            }

            int panelIndex = allEventsPanel.Controls.IndexOf(removeEventButton.Parent);
            if (panelIndex != -1)
            {
                m_calendarData.GetEventsData().RemoveAt(panelIndex);
                allEventsPanel.Controls.RemoveAt(panelIndex);
            }
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            EventData newEventData = new EventData();
            newEventData.SetMonthDataCallback(m_calendarData.GetMonthsData);

            m_calendarData.GetEventsData().Add(newEventData);

            addFixedEventUI(newEventData);
        }
    }
}
