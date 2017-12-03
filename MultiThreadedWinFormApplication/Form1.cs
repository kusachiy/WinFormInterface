using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadedWinFormApplication
{
    public partial class Form1 : Form
    {
        private Worker _workerSingle;
        private Worker _workerMultiply;
        private Thread _thread;


        public Form1()
        {
            InitializeComponent();
            _workerSingle = new Worker();
            _workerSingle.ProgressChanged += _worker_ProgressChangedSingle;
            _workerSingle.EndOfWork += _worker_EndOfWorkSingle;
            _workerMultiply = new Worker();
            _workerMultiply.ProgressChanged += _worker_ProgressChangedMultiply;
            _workerMultiply.EndOfWork += _worker_EndOfWorkMultiply;
            progressBar1.Maximum = Worker.MAXIMUM;
            progressBar2.Maximum = Worker.MAXIMUM;
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            _workerSingle.DoWork();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _workerSingle.Reset();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _workerSingle.Stop();
        }

        private void _worker_EndOfWorkSingle(bool isStopped)
        {
            var result = isStopped ? "Выполнение остановлено" : "Конец работы";
            MessageBox.Show(result);
        }

        private void _worker_ProgressChangedSingle(int value)
        {
            progressBar1.Value = value;
        }



        private void StartM_Click(object sender, EventArgs e)
        {
            _thread = new Thread(_workerMultiply.DoWork);
            _thread.Start();
        }

        private void ResetM_Click(object sender, EventArgs e)
        {
            _workerMultiply.Reset();
        }

        private void StopM_Click(object sender, EventArgs e)
        {
            _workerMultiply.Stop();
        }

        private void _worker_ProgressChangedMultiply(int value)
        {
            Action action = () =>
            {
                progressBar2.Value = value;
            };
            Invoke(action);
        }
        private void _worker_EndOfWorkMultiply(bool isStopped)
        {
            Invoke(
                new Action(() =>
                    {
                        var result = isStopped ? "Выполнение остановлено" : "Конец работы";
                        MessageBox.Show(result);
                    }
                ));
        }
    }
}
