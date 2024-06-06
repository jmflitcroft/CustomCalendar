using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace CustomCalendar
{
    public partial class Form1 : Form
    {
        private List<DayData> m_daysData = new List<DayData>();
        private List<MonthData> m_monthsData = new List<MonthData>();

        public Form1()
        {
            InitializeComponent();

            if (pageLayoutComboBox.Items.Count > 0)
            {
                pageLayoutComboBox.SelectedIndex = 0;
            }
        }


        private void addDayButton_Click(object sender, EventArgs e)
        {
            DayData newDayData = new DayData();

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
            dayNameTextBox.Location = new System.Drawing.Point(enterDayNameLabel.Size.Width, 5);
            dayNameTextBox.Size = new Size(backgroundSize.Width - dayNameTextBox.Location.X - 10, dayNameTextBox.Size.Height);
            dayNameTextBox.TextChanged += new EventHandler(newDayData.DayNameText_Changed);
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

            m_daysData.Add(newDayData);
        }

        private void addMonthButton_Click(object sender, EventArgs e)
        {
            MonthData newMonthData = new MonthData();

            Panel newPanel = new Panel();
            newPanel.BorderStyle = BorderStyle.FixedSingle;
            Size backgroundSize = new Size(allMonthsFlowPanel.Width - 8, 0);

            // Month Name
            Label monthNameLabel = new Label();
            monthNameLabel.Text = "Month Name: ";
            monthNameLabel.AutoSize = true;
            newPanel.Controls.Add(monthNameLabel);

            TextBox monthNameTextBox = new TextBox();

            monthNameTextBox.TextChanged += new EventHandler(newMonthData.MonthNameText_Changed);
            newPanel.Controls.Add(monthNameTextBox);

            // Number of Days
            Label numberOfDaysLabel = new Label();
            numberOfDaysLabel.Text = "Number of Days: ";
            numberOfDaysLabel.AutoSize = true;
            numberOfDaysLabel.Location = new System.Drawing.Point(0, monthNameTextBox.Bottom);
            newPanel.Controls.Add(numberOfDaysLabel);

            TextBox numberOfDaysTextBox = new TextBox();
            numberOfDaysTextBox.TextChanged += new EventHandler(newMonthData.NumberOfDaysText_Changed);
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
            newPanel.Controls.Add(removeMonthButton);


            backgroundSize.Height = removeMonthButton.Bottom + 10;
            newPanel.Size = backgroundSize;
            newPanel.Size = backgroundSize;
            allMonthsFlowPanel.Controls.Add(newPanel);

            m_monthsData.Add(newMonthData);

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
                m_daysData.RemoveAt(panelIndex);
                allDaysFlowPanel.Controls.RemoveAt(panelIndex);
            }
        }

        private void generatePDFButton_Click(object sender, EventArgs e)
        {
            string pdfDestination = "Calendar.pdf";

            PdfWriter pdfWriter = new PdfWriter(pdfDestination);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);

            Document document = new Document(pdfDocument, getPageSize());
            document.SetMargins(20, 20, 20, 20);

            int monthDayStartingIndex = startingDayComboBox.SelectedIndex >= 0 ? startingDayComboBox.SelectedIndex : 0;

            for (int i = 0; i < m_monthsData.Count; ++i)
            {
                MonthData monthData = m_monthsData[i];
                createMonthPDFTable(document, monthData, monthDayStartingIndex);

                monthDayStartingIndex = (monthDayStartingIndex + monthData.GetNumberOfDays()) % m_daysData.Count;

                if (i < m_monthsData.Count - 1)
                {
                    document.Add(new AreaBreak());
                }
            }

            pdfDocument.Close();
        }



        private void createMonthPDFTable(Document document, MonthData monthData, int startingDayIndex)
        {
            Table header = new Table(2);
            header.SetWidth(UnitValue.CreatePercentValue(100));
            header.AddCell(new Cell().Add(new Paragraph("Month: " + monthData.GetMonthName())));
            header.AddCell(new Cell().Add(new Paragraph("Year: " + 0)));
            header.SetPaddingBottom(10);
            header.SetHeight(UnitValue.CreatePointValue(30));
            document.Add(header);

            UnitValue headerHeight = header.GetHeight();

            var table = new Table(m_daysData.Count);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            
            table.SetHeight(getPageSize().GetHeight() - header.GetHeight().GetValue());



            foreach (DayData day in m_daysData)
            {
                table.AddHeaderCell(day.GetDayName());
            }
            for (int i = 0; i < monthData.GetNumberOfDays() + startingDayIndex; ++i)
            {
                Cell cell = new Cell();
                if (i < startingDayIndex)
                {
                    cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
                }
                else
                {
                    cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.WHITE);
                    cell.Add(new Paragraph("" + (i - startingDayIndex + 1)));
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
                startingDayComboBox.Items.Clear();

                foreach (DayData day in m_daysData)
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
                    startingDayComboBox.SelectedIndex = Math.Clamp(startingDayComboBox.SelectedIndex, 0, startingDayComboBox.Items.Count - 1);
                }
            }
        }
    }
}
