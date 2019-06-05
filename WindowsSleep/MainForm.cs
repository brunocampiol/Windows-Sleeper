using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsSleep.Models;
using WindowsSleep.Static;

namespace WindowsSleep
{
    public partial class MainForm : Form
    {
        private Timer _timerEvent;
        private TimeSpan _remainingTime;

        //private DateTime _startedDate;
        //private DateTime _remainingDate;

        private static TimeSpan _secondInterval = new TimeSpan(0, 0, 1);

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_timerEvent == null)
            {
                _timerEvent = new Timer();
                _timerEvent.Interval = 1000;
                _timerEvent.Tick += _timerEvent_Tick;

                foreach (WindowsEventType type in (WindowsEventType[])Enum.GetValues(typeof(WindowsEventType))) cmbEventType.Items.Add(type);
                foreach (TimeType type in (TimeType[])Enum.GetValues(typeof(TimeType))) cmbTimeType.Items.Add(type);
                cmbEventType.SelectedIndex = 0;
                cmbTimeType.SelectedIndex = 0;
            }
            
            lblTimeRemaining.Text = Constants.Ready;
            btnCancel.Enabled = false;
        }

        private void RefreshUI()
        {
            if (_timerEvent.Enabled)
            {
                lblTimeRemaining.Text = _remainingTime.TotalSeconds.ToString() + Constants.SecondsRemaining;

                cmbEventType.Enabled = false;
                cmbTimeType.Enabled = false;
                btnStart.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                cmbEventType.Enabled = true;
                cmbTimeType.Enabled = true;
                btnStart.Enabled = true;
                btnCancel.Enabled = false;

                InitializeUI();
            }
        }

        private void _timerEvent_Tick(object sender, EventArgs e)
        {
            if (_remainingTime.Ticks > 0)
            {
                _remainingTime = _remainingTime.Subtract(_secondInterval);
            }
            else
            {
                _timerEvent.Stop();

                WindowsEvents.DoWindowEvent((WindowsEventType)cmbEventType.SelectedItem);
            }

            RefreshUI();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // TODO validate input
            var inputTime = txtEventTime.Text;
            TimeType timeType = (TimeType)cmbTimeType.SelectedItem;
            
            _remainingTime = TimeTypeHelper.GetTimeSpan(inputTime, timeType);
            
            _timerEvent.Start();

            cmbEventType.Enabled = false;
            cmbTimeType.Enabled = false;
            btnStart.Enabled = false;
            btnCancel.Enabled = true;

            RefreshUI();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _timerEvent.Stop();

            cmbEventType.Enabled = true;
            cmbTimeType.Enabled = true;
            btnStart.Enabled = true;
            btnCancel.Enabled = false;

            RefreshUI();
        }
    }
}
