using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.IO;
using System.Text;
using System.Windows;
using System.Linq;

using System.Threading.Tasks;

namespace Airhub.files
{
    internal class RunProg
    {
        static void CallBot()
        {

            // Specify the path to your Python file
            string pythonScript = @"D:\test.py";

            // Create a process start info
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "python";
            psi.Arguments = pythonScript;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            // Start the Python process
            Process pythonProcess = Process.Start(psi);

            // Capture the standard output and error
            StringBuilder output = new StringBuilder();
            pythonProcess.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    output.AppendLine(e.Data);
                    // Update your UI with the output here
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Update UI with the output
                        // For example, you could update a TextBox or a TextBlock
                        // textBox.Text += e.Data + Environment.NewLine;
                    });
                }
            };
            pythonProcess.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    output.AppendLine(e.Data);
                    // Update your UI with the error message here
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Update UI with the error message
                        // For example, you could display it in a MessageBox
                        // MessageBox.Show(e.Data);
                    });
                }
            };

            pythonProcess.BeginOutputReadLine();
            pythonProcess.BeginErrorReadLine();

            // Wait for the process to exit
            pythonProcess.WaitForExit();

            // Once the process exits, you can access the captured output
            string capturedOutput = output.ToString();
        }
    }
}
