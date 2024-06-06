
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomCalendar
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PublishToPDF()
        {

        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainTabsControl = new TabControl();
            daysTabPage = new TabPage();
            splitContainer1 = new SplitContainer();
            allDaysFlowPanel = new FlowLayoutPanel();
            panel1 = new Panel();
            addDayButton = new Button();
            monthTabPage = new TabPage();
            splitContainer2 = new SplitContainer();
            allMonthsFlowPanel = new FlowLayoutPanel();
            panel2 = new Panel();
            button1 = new Button();
            eventsTabPage = new TabPage();
            finalizeTabPage = new TabPage();
            label2 = new Label();
            label1 = new Label();
            startingDayComboBox = new ComboBox();
            pageLayoutComboBox = new ComboBox();
            button2 = new Button();
            mainTabsControl.SuspendLayout();
            daysTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            monthTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            panel2.SuspendLayout();
            finalizeTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // mainTabsControl
            // 
            mainTabsControl.Controls.Add(daysTabPage);
            mainTabsControl.Controls.Add(monthTabPage);
            mainTabsControl.Controls.Add(eventsTabPage);
            mainTabsControl.Controls.Add(finalizeTabPage);
            mainTabsControl.Dock = DockStyle.Fill;
            mainTabsControl.Location = new System.Drawing.Point(0, 0);
            mainTabsControl.Name = "mainTabsControl";
            mainTabsControl.SelectedIndex = 0;
            mainTabsControl.Size = new Size(800, 450);
            mainTabsControl.TabIndex = 2;
            mainTabsControl.Selected += tabControl_Selected;
            // 
            // daysTabPage
            // 
            daysTabPage.Controls.Add(splitContainer1);
            daysTabPage.Location = new System.Drawing.Point(4, 24);
            daysTabPage.Name = "daysTabPage";
            daysTabPage.Padding = new Padding(3);
            daysTabPage.Size = new Size(792, 422);
            daysTabPage.TabIndex = 0;
            daysTabPage.Text = "Days";
            daysTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(allDaysFlowPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(786, 416);
            splitContainer1.SplitterDistance = 525;
            splitContainer1.TabIndex = 2;
            // 
            // allDaysFlowPanel
            // 
            allDaysFlowPanel.AutoScroll = true;
            allDaysFlowPanel.Dock = DockStyle.Fill;
            allDaysFlowPanel.Location = new System.Drawing.Point(0, 0);
            allDaysFlowPanel.Margin = new Padding(0);
            allDaysFlowPanel.Name = "allDaysFlowPanel";
            allDaysFlowPanel.Size = new Size(525, 416);
            allDaysFlowPanel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(addDayButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(257, 416);
            panel1.TabIndex = 2;
            // 
            // addDayButton
            // 
            addDayButton.Dock = DockStyle.Top;
            addDayButton.Location = new System.Drawing.Point(0, 0);
            addDayButton.Name = "addDayButton";
            addDayButton.Size = new Size(257, 23);
            addDayButton.TabIndex = 1;
            addDayButton.Text = "Add Day";
            addDayButton.UseVisualStyleBackColor = true;
            addDayButton.Click += addDayButton_Click;
            // 
            // monthTabPage
            // 
            monthTabPage.Controls.Add(splitContainer2);
            monthTabPage.Location = new System.Drawing.Point(4, 24);
            monthTabPage.Name = "monthTabPage";
            monthTabPage.Padding = new Padding(3);
            monthTabPage.Size = new Size(792, 422);
            monthTabPage.TabIndex = 1;
            monthTabPage.Text = "Months";
            monthTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(allMonthsFlowPanel);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(panel2);
            splitContainer2.Size = new Size(786, 416);
            splitContainer2.SplitterDistance = 525;
            splitContainer2.TabIndex = 3;
            // 
            // allMonthsFlowPanel
            // 
            allMonthsFlowPanel.AutoScroll = true;
            allMonthsFlowPanel.Dock = DockStyle.Fill;
            allMonthsFlowPanel.Location = new System.Drawing.Point(0, 0);
            allMonthsFlowPanel.Name = "allMonthsFlowPanel";
            allMonthsFlowPanel.Size = new Size(525, 416);
            allMonthsFlowPanel.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(257, 416);
            panel2.TabIndex = 0;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Top;
            button1.Location = new System.Drawing.Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(257, 23);
            button1.TabIndex = 2;
            button1.Text = "Add Month";
            button1.UseVisualStyleBackColor = true;
            button1.Click += addMonthButton_Click;
            // 
            // eventsTabPage
            // 
            eventsTabPage.Location = new System.Drawing.Point(4, 24);
            eventsTabPage.Name = "eventsTabPage";
            eventsTabPage.Padding = new Padding(3);
            eventsTabPage.Size = new Size(792, 422);
            eventsTabPage.TabIndex = 2;
            eventsTabPage.Text = "Events";
            eventsTabPage.UseVisualStyleBackColor = true;
            // 
            // finalizeTabPage
            // 
            finalizeTabPage.Controls.Add(label2);
            finalizeTabPage.Controls.Add(label1);
            finalizeTabPage.Controls.Add(startingDayComboBox);
            finalizeTabPage.Controls.Add(pageLayoutComboBox);
            finalizeTabPage.Controls.Add(button2);
            finalizeTabPage.Location = new System.Drawing.Point(4, 24);
            finalizeTabPage.Name = "finalizeTabPage";
            finalizeTabPage.Padding = new Padding(3);
            finalizeTabPage.Size = new Size(792, 422);
            finalizeTabPage.TabIndex = 3;
            finalizeTabPage.Text = "Finalize";
            finalizeTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 53);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 6;
            label2.Text = "Starting Day:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 19);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 5;
            label1.Text = "Page Layout:";
            // 
            // startingDayComboBox
            // 
            startingDayComboBox.FormattingEnabled = true;
            startingDayComboBox.Location = new System.Drawing.Point(117, 53);
            startingDayComboBox.Name = "startingDayComboBox";
            startingDayComboBox.Size = new Size(121, 23);
            startingDayComboBox.TabIndex = 4;
            // 
            // pageLayoutComboBox
            // 
            pageLayoutComboBox.FormattingEnabled = true;
            pageLayoutComboBox.Items.AddRange(new object[] { "Portrait", "Landscape" });
            pageLayoutComboBox.Location = new System.Drawing.Point(117, 19);
            pageLayoutComboBox.Name = "pageLayoutComboBox";
            pageLayoutComboBox.Size = new Size(121, 23);
            pageLayoutComboBox.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(46, 108);
            button2.Name = "button2";
            button2.Size = new Size(162, 23);
            button2.TabIndex = 2;
            button2.Text = "Generate";
            button2.UseVisualStyleBackColor = true;
            button2.Click += generatePDFButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainTabsControl);
            Name = "Form1";
            Text = "Form1";
            mainTabsControl.ResumeLayout(false);
            daysTabPage.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            monthTabPage.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            finalizeTabPage.ResumeLayout(false);
            finalizeTabPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl mainTabsControl;
        private TabPage daysTabPage;
        private TabPage monthTabPage;
        private FlowLayoutPanel allDaysFlowPanel;
        private Button button1;
        private FlowLayoutPanel allMonthsFlowPanel;
        private TabPage eventsTabPage;
        private TabPage finalizeTabPage;
        private Button addDayButton;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private SplitContainer splitContainer2;
        private Panel panel2;
        private Button button2;
        private ComboBox pageLayoutComboBox;
        private ComboBox startingDayComboBox;
        private Label label2;
        private Label label1;
    }
}
