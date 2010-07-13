using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace WinPure.ContactManagement.Common
{
    public class SynchronisedObservableCollection<T> : ICollection<T>,
                                                        INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly ICollection<T> collection;
        private readonly Dispatcher dispatcher;

        public SynchronisedObservableCollection(ICollection<T> collection)
        {
            if (collection == null ||
                collection as INotifyCollectionChanged == null ||
                collection as INotifyPropertyChanged == null)
            {
                throw new ArgumentException("Collection must support ICollection, INotifyCollectionChanged " +
                                            " and INotifyPropertyChanged interfaces.");
            }

            this.collection = collection;
            dispatcher = Dispatcher.CurrentDispatcher;

            var collectionChanged = collection as INotifyCollectionChanged;
            collectionChanged.CollectionChanged += delegate(Object sender, NotifyCollectionChangedEventArgs e)
            {
                dispatcher.Invoke(DispatcherPriority.Normal,
                                  new RaiseCollectionChangedEventHandler(
                                      RaiseCollectionChangedEvent), e);
            };

            var propertyChanged = collection as INotifyPropertyChanged;
            propertyChanged.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
            {
                dispatcher.Invoke(DispatcherPriority.Normal,
                                  new RaisePropertyChangedEventHandler(
                                      RaisePropertyChangedEvent), e);
            };
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return collection.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)collection).GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaiseCollectionChangedEvent(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        private void RaisePropertyChangedEvent(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #region Nested type: RaiseCollectionChangedEventHandler

        private delegate void RaiseCollectionChangedEventHandler(NotifyCollectionChangedEventArgs e);

        #endregion

        #region Nested type: RaisePropertyChangedEventHandler

        private delegate void RaisePropertyChangedEventHandler(PropertyChangedEventArgs e);

        #endregion
    }
}
