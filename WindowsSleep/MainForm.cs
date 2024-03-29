﻿using System;
using System.Windows.Forms;
using WindowsSleep.Models;
using WindowsSleep.Static;

namespace WindowsSleep
{
    public partial class MainForm : Form
    {
        private Timer _timerEvent;
        private TimeSpan _remainingTime;

        public MainForm()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _timerEvent = new Timer();
            _timerEvent.Interval = 1000;
            _timerEvent.Tick += TimerEvent_Tick;

            foreach (WindowsEventType type in (WindowsEventType[])Enum.GetValues(typeof(WindowsEventType))) cmbEventType.Items.Add(type);
            foreach (TimeType type in (TimeType[])Enum.GetValues(typeof(TimeType))) cmbTimeType.Items.Add(type);
            cmbEventType.SelectedIndex = 0;
            cmbTimeType.SelectedIndex = 0;

            // keyboard messages are received by the form before they reach any controls on the form
            this.KeyPreview = true;
            this.KeyPress += MainForm_KeyPress;

            txtEventTime.KeyPress += TxtEventTime_KeyPress;

            btnCancel.Enabled = false;
        }

        private void RefreshUI()
        {
            if (_timerEvent.Enabled)
            {
                lblTimeRemaining.Text = _remainingTime.TotalSeconds.ToString() + Constants.SecondsRemaining;

                txtEventTime.Enabled = false;
                cmbEventType.Enabled = false;
                cmbTimeType.Enabled = false;
                btnStart.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                txtEventTime.Enabled = true;
                cmbEventType.Enabled = true;
                cmbTimeType.Enabled = true;
                btnStart.Enabled = true;
                btnCancel.Enabled = false;

                lblTimeRemaining.Text = Constants.Ready;
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && _timerEvent.Enabled == false) btnStart_Click(null, null);
            else if (e.KeyChar == (char)Keys.Enter && _timerEvent.Enabled == true) btnCancel_Click(null, null);
            else if (e.KeyChar == (char)Keys.Escape && _timerEvent.Enabled == true) btnCancel_Click(null, null);
        }

        private void TxtEventTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TimerEvent_Tick(object sender, EventArgs e)
        {
            if (_remainingTime.Ticks > 0)
            {
                _remainingTime = _remainingTime.Subtract(Constants.ONE_SECOND);
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
            if (!IsValidInput(txtEventTime.Text))
            {
                lblTimeRemaining.Text = "'" + txtEventTime.Text + "'" + Constants.InvalidInput;
                return;
            }

            // TODO validate input
            var inputTime = txtEventTime.Text;
            TimeType timeType = (TimeType)cmbTimeType.SelectedItem;

            _remainingTime = TimeTypeHelper.GetTimeSpan(inputTime, timeType);

            _timerEvent.Start();

            txtEventTime.Enabled = false;
            cmbEventType.Enabled = false;
            cmbTimeType.Enabled = false;
            btnStart.Enabled = false;
            btnCancel.Enabled = true;

            this.btnCancel.Focus();

            RefreshUI();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _timerEvent.Stop();

            txtEventTime.Enabled = true;
            cmbEventType.Enabled = true;
            cmbTimeType.Enabled = true;
            btnStart.Enabled = true;
            btnCancel.Enabled = false;

            this.btnStart.Focus();

            RefreshUI();
        }

        private bool IsValidInput(string input)
        {
            int result;

            return int.TryParse(input, out result);
        }
    }
}
