using log4net.Util;
using MetroFramework.Forms;
using my.domain.lib.core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.Extensions
{
    public static class MessageBoxExt
    {
        public static void ShowWarning (string Message)
        {
            Program.Logger.Info(Message);

            MessageBox.Show(Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowWarning(WarningException Message)
        {
            Program.Logger.Info(Message);

            MessageBox.Show(Message.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowWarning(AggregateException Message)
        {
            Program.Logger.Info(Message);

            string msgs = "";
            foreach (var x in Message.InnerExceptions)
                msgs += x.Message;

            MessageBox.Show(msgs, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        
        public static bool ShowQuestion(string Message)
        {
           var res =  MessageBox.Show(Message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           return res == DialogResult.Yes;
        }
        public static DialogResult ShowYesNoCancel(string Message)
        {
            return  MessageBox.Show(Message, "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
        public static void ShowException(Exception x,string Message="")
        {
            string msg = string.Format("{0}{1}{2}", Message, Message != "" ? "\r\n\r\n" : "", x.Message);
           // msg += "\r\n" + x.StackTrace;
           // msg += "\r\nSOURCE:" + x.Source;

            if (x.InnerException!=null)
            { msg += "\r\n" + x.InnerException.Message;
               // msg += "\r\n" + x.InnerException.StackTrace;
              //  msg += "\r\n" + x.InnerException.Source;
            }

            if (x.GetType().Equals(typeof(SchemaException)))
            {
                SchemaException cx = x as SchemaException;

                msg += "\r\n" + cx.ExceptionMessage;

                msg += "\r\n" + cx.BaseException.Message;

                if (cx.BaseException.InnerException != null)
                    msg += "\r\n" + cx.BaseException.InnerException.Message;

                Program.Logger.Fatal(msg, cx.BaseException);
            }

            if (x.GetType().Equals(typeof(ConnectionException)))
            {
                ConnectionException cx = x as ConnectionException;

                msg += "\r\n" + cx.ExceptionMessage;

                msg += "\r\n" + cx.BaseException.Message;

                if (cx.BaseException.InnerException != null)
                    msg += "\r\n" + cx.BaseException.InnerException.Message;

                Program.Logger.Fatal(msg, cx.BaseException);
            }

            Program.Logger.Fatal(msg, x);

            msg += "\r\n\r\n" + "Click the Yes button to view the error log.";

            if (MessageBox.Show(msg, "Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                ////Log the users system information for debug purposes
                //Program.logger.Info(Licensing.SystemInfo.SystemInformation(string.Empty));

                Program.ShowLog();

            };

           
        }
        public static void ShowInformation(string Message)
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowInvalidLicense(string Message="")
        {
            MessageBox.Show(string.Format("Your licences has expired. Please renew your licence key to enable this function.\r\n {0}",Message), "Licence Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowInput(Form parent, string Message,ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(300, 150);
            MetroForm inputBox = new MetroForm();

            inputBox.ClientSize = size;
            inputBox.SubTitle = "Input";
            inputBox.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            inputBox.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            inputBox.Resizable = false;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
           


            System.Windows.Forms.Label label = new Label();
            label.Size = new System.Drawing.Size(size.Width - 30, 23);
            label.Location = new System.Drawing.Point(15, 35);
            label.Text = Message;
            inputBox.Controls.Add(label);

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 30, 23);
            textBox.Location = new System.Drawing.Point(15, 60);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width/2 - 80, size.Height-50);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width/2 + 10, size.Height - 50);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog(parent);
            input = textBox.Text;
            return result;
        }

    }
}
