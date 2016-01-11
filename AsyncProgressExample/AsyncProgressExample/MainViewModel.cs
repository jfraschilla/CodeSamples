using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncProgressExample
{
    public class MainViewModel :BindableBase
    {
        private CancellationTokenSource _cts;
        private bool _isBusy;
        private int _pctComplete;

        public ICommand BeginCommand { get; private set; }
        public ICommand InteractCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public MainViewModel()
        {
            BeginCommand = new DelegateCommand(OnBeginButtonClick);
            InteractCommand = new DelegateCommand(OnInteractButtonClick);
            CancelCommand = new DelegateCommand(OnCancelButtonClick);
            IsBusy = false;
        }

        private void OnCancelButtonClick()
        {
            _cts.Cancel();
            IsBusy = false;
        }

        private void OnInteractButtonClick()
        {
            
        }

        private void OnBeginButtonClick()
        {
            IsBusy = true;
            var progressIndicator = new Progress<int>(ReportProgress);
            _cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                await DoLongRunningProcessAsync(progressIndicator, _cts.Token);
                IsBusy = false;
            });
            
        }

        private void ReportProgress(int value)
        {
            PercentComplete = value;
        }

        private async Task DoLongRunningProcessAsync(IProgress<int> progress, CancellationToken cancelToken = new CancellationToken())
        {
            for (int i=0; i<10; i++)
            {
                cancelToken.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                progress.Report(i * 10);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public int PercentComplete
        {
            get { return _pctComplete; }
            set { SetProperty(ref _pctComplete, value); }
        }
    }
}
