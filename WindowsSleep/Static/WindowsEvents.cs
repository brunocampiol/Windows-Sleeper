using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsSleep.Models;

namespace WindowsSleep.Static
{
    public static class WindowsEvents
    {
        //[DllImport("user32")]
        //public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        //[DllImport("user32")]
        //public static extern void LockWorkStation();

        public static void DoWindowEvent(WindowsEventType eventType)
        {
            switch (eventType)
            {
                case WindowsEventType.Reboot:
                    Reboot();
                    break;
                case WindowsEventType.Shutdown:
                    Shutdown();
                    break;
                case WindowsEventType.Sleep:
                    Sleep();
                    break;
                default:
                    Sleep();
                    break;
            }
        }

        // Shutdown.
        public static void Shutdown()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        // Reboot.
        public static void Reboot()
        {
            var psi = new ProcessStartInfo("shutdown", "/r /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        // Log off.
        //public static void btnLogOff_Click(object sender, EventArgs e)
        //{
        //    ExitWindowsEx(0, 0);
        //}

        //// Lock.
        //public static void btnLock_Click(object sender, EventArgs e)
        //{
        //    LockWorkStation();
        //}

        // Hibernate.
        //public static void Hibernate(object sender, EventArgs e)
        //{
        //    Application.SetSuspendState(PowerState.Hibernate, true, true);
        //}

        // Sleep.
        public static void Sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }
    }
}
