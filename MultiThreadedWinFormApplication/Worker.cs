using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadedWinFormApplication
{
    class Worker
    {
        public const int MAXIMUM = 10;
        public const int SPEED = 400;

        private bool _isStopped = false;
        private int _value = 0;

        public void DoWork()
        {
            _isStopped = false;
            while (_value < MAXIMUM)
            {
                if (_isStopped)
                    break;
                _value++;
                ProgressChanged(_value);
                Thread.Sleep(SPEED);
            }
            EndOfWork(_isStopped);
        }

        public void Reset()
        {
            _value = 0;
            ProgressChanged(_value);
        }

        public void Stop()
        {
            _isStopped = true;
            ProgressChanged(_value);
        }

        public event Action<int> ProgressChanged;

        public event Action<bool> EndOfWork;

    }
}
