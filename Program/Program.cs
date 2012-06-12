using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Windows.Forms;


namespace CAPDesktopAlert
{
    class Program
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static int alarmCounter = 1;
        static bool exitFlag = false;

        // This is the method to run when the timer is raised.
        private static void TimerEventProcessor(Object myObject,
                                                EventArgs myEventArgs)
        {
            // After timer has completed, stop timer and close program
            myTimer.Stop();
            exitFlag = true;
            Application.Exit();
        }

        static void Main(string[] args)
        {
            

                // Loads the XML file.  File can either be local on the machine or read from an XML file on the web.
                XmlDocument xmlDocument = new XmlDocument();
                //xmlDocument.Load("http://www.getrave.com/cap/kirtland/channel2");
                string url_arg = args[0].Replace(@"\", @"\\");
                xmlDocument.Load(url_arg);

                // Gets the contents of certain CAP tags
                var effectiveText = xmlDocument.GetElementsByTagName("effective");
                var expireText = xmlDocument.GetElementsByTagName("expires");
                var eventText = xmlDocument.GetElementsByTagName("event");
                var descripText = xmlDocument.GetElementsByTagName("description");

                //Gets the current Time and converts required strings to Datetime
                DateTime now = DateTime.Now;
                DateTime effective = Convert.ToDateTime(effectiveText[0].InnerText);
                DateTime expires = Convert.ToDateTime(expireText[0].InnerText);

                //Compares the current date to the start time.  Also compares current date to end and gets the number of milliseconds between now and the expire time for the timer
                int effectiveTimeDiff = DateTime.Compare(now, effective);
                int expireTimeDiff = DateTime.Compare(now, expires);
                TimeSpan span = expires - now;
                int ms = Convert.ToInt32(span.TotalMilliseconds);
                
                // Checks to see if now is greater than the start time and less than the expire time
                if ((effectiveTimeDiff >= 0) && (expireTimeDiff < 0))
                {
                    //builds form to display XML text
                    Form alert = new Form();

                    alert.WindowState = FormWindowState.Maximized;
                    alert.FormBorderStyle = FormBorderStyle.None;
                    alert.TopMost = true;
                    alert.BackColor = System.Drawing.Color.White;

                    GrowLabel AlertTitle = new GrowLabel();
                    GrowLabel AlertDescription = new GrowLabel();

                    AlertTitle.Width = 1400;
                    AlertTitle.Top = 100;
                    AlertTitle.Text = eventText[0].InnerText;
                    AlertTitle.Font = new System.Drawing.Font("Arial", AlertTitle.Font.Size + 80);
                    AlertTitle.ForeColor = System.Drawing.Color.Red;
                    AlertTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;

                    AlertDescription.Width = 1400;
                    AlertDescription.Top = 450;
                    AlertDescription.Text = descripText[0].InnerText;
                    AlertDescription.Font = new System.Drawing.Font("Arial", AlertDescription.Font.Size + 40);
                    AlertDescription.ForeColor = System.Drawing.Color.Red;
                    AlertDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;

                    // Places title and description taken from XML within form
                    AlertTitle.Parent = alert;
                    AlertDescription.Parent = alert;

                    // Creates a timer with the number of milliseconds between now and expire time

                    myTimer.Tick += new EventHandler(TimerEventProcessor);

                    myTimer.Interval = ms;
                    myTimer.Start();
                    
                    //  Displays the form while the timer has not yet expired
                     while (exitFlag == false)
                     {
                        alert.ShowDialog();
                     }

                   
                }
               
            }
                
        
        
        
       
    }
}
