using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnhancedObservableCollection;

namespace EnhancedObservableCollection.MainWindowVM
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow View;

        public MainWindowVM()
        {
            NormalContainer = new ObservableCollection<int>();
            NormalList = new List<int>();
            EnhancedContainer = new EnhancedObservableCollection<int>();
        }

        #region View-Binded Propertise
        private ObservableCollection<int> _normalContainer = null;
        public ObservableCollection<int> NormalContainer
        {
            get { return _normalContainer; }
            set
            {
                _normalContainer = value;
                OnPropertyChanged("NormalContainer");
            }
        }

        private List<int> _normalList = null;
        public List<int> NormalList
        {
            get { return _normalList; }
            set
            {
                _normalList = value;
                OnPropertyChanged("NormalList");
            }
        }

        private EnhancedObservableCollection<int> _enhancedContainer = null;
        public EnhancedObservableCollection<int> EnhancedContainer
        {
            get { return _enhancedContainer; }
            set
            {
                _enhancedContainer = value;
                OnPropertyChanged("EnhancedContainer");
            }
        }

        #endregion

        #region Methods
        private void CheckPerformance(int _btnNumber)
        {
            switch(_btnNumber)
            {
                case 1:
                    {
                        _normalContainer.Clear();
                        System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();

                        _stopWatch.Reset();         // 초기화
                        _stopWatch.Start();

                        for (int i = 0; i < 1000; i++)
                        {
                            NormalContainer.Add(i);
                        }

                        _stopWatch.Stop();
                        string output = _stopWatch.ElapsedMilliseconds.ToString() + "ms";

                        MessageBox.Show(output);
                    }
                    break;
                case 2:
                    {
                        NormalList.Clear();
                        System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();

                        _stopWatch.Reset();         // 초기화
                        _stopWatch.Start();

                        for (int i = 0; i < 1000; i++)
                        {
                            NormalList.Add(i);
                        }

                        _stopWatch.Stop();
                        string output = _stopWatch.ElapsedMilliseconds.ToString() + "ms";

                        MessageBox.Show(output);
                    }
                    break;
                case 3:
                    {
                        EnhancedContainer.Clear();
                        System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
                        
                        _stopWatch.Reset();         // 초기화
                        _stopWatch.Start();


                        EnhancedContainer.IsSuspend = true;

                        for( int i = 0; i < 1000; i++ )
                        {
                            EnhancedContainer.Add(i);
                        }

                        EnhancedContainer.IsSuspend = false;
                        EnhancedContainer.Refresh();

                        _stopWatch.Stop();
                        string output = _stopWatch.ElapsedMilliseconds.ToString() + "ms";

                        MessageBox.Show(output);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region Command
        private RelayCommand _onClickBtn1 = null;
        public ICommand OnClickBtn1
        {
            get
            {
                if(_onClickBtn1 == null )
                {
                    _onClickBtn1 = new RelayCommand( _ => 
                    {
                        CheckPerformance(1);
                    } , _ => true);
                }
                return _onClickBtn1;
            }
        }

        private RelayCommand _onClickBtn2 = null;
        public ICommand OnClickBtn2
        {
            get
            {
                if (_onClickBtn2 == null)
                {
                    _onClickBtn2 = new RelayCommand(_ =>
                    {
                        CheckPerformance(2);
                    }, _ => true);
                }
                return _onClickBtn2;
            }
        }

        private RelayCommand _onClickBtn3 = null;
        public ICommand OnClickBtn3
        {
            get
            {
                if (_onClickBtn3 == null)
                {
                    _onClickBtn3 = new RelayCommand(_ =>
                    {
                        CheckPerformance(3);
                    }, _ => true);
                }
                return _onClickBtn3;
            }
        }

        #endregion

        #region Property Changed Implement
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region RelayCommand Implement
        public class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }
        #endregion
    }

}
