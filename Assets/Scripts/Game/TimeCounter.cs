using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class TimeCounter
    {
        private bool _isPause = false;
        private CancellationTokenSource _cancellationTokenSource;
        public event Action<int> OnTimeChange;
    
        public async UniTask BeginCount(int second)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        
            _cancellationTokenSource = new CancellationTokenSource();
        
            var currentSec = second;
            while (currentSec > 0)
            {
                if (!_isPause)
                {
                    await UniTask.Delay(1000, cancellationToken: _cancellationTokenSource.Token);
                    currentSec--;
                    OnTimeChange?.Invoke(currentSec);
                }
                else
                {
                    await UniTask.Yield();
                }
            }
        }

        public void Pause()
        {
            _isPause = true;
        }

        public void Resume()
        {
            _isPause = false;
        }
    }
}