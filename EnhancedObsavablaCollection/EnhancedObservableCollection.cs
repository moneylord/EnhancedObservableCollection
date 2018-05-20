using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnhancedObservableCollection
{
    public class EnhancedObservableCollection<T> : ObservableCollection<T>
    {
        public bool IsSuspend { get; set; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsSuspend == false) base.OnCollectionChanged(e);
        }
        public void Refresh()
        {
            IsSuspend = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
