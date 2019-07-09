using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventCreator.Dto
{
    public class CycleCreator : IDisposable
    {

        private readonly JsonToRabbit _jsonToRabbit = new JsonToRabbit();
        private readonly Random _random = new Random();
        private readonly Timer _timer;

        public CycleCreator()
        {
            _timer = new Timer(TimerCallback, null, 800, Timeout.Infinite);
        }

        public void TimerCallback(object obj)
        {
            try
            {
                var json = RandomChoise.SomethingActionsWithUsers();
                var n = _random.Next(0, 2);
                if (n == 0)
                {
                    JsonToFile.WriteJson(json);
                }
                else
                {
                    _jsonToRabbit.AddToRabbit(json);
                }
            }
            finally
            {
                _timer.Change(800, Timeout.Infinite);
            }
        }

        public void Dispose()
        {

            _timer?.Dispose();
            _jsonToRabbit?.Dispose();

        }
    }
}
