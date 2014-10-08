using System;
using System.Collections;

namespace EC.UI.WebControls
{
    internal sealed class DummyDataSource : ICollection
    {
        private int dataItemCount;
        public DummyDataSource(int dataItemCount)
        {
            this.dataItemCount = dataItemCount;
        }
        public int Count
        {
            get
            {
                return dataItemCount;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }
        public object SyncRoot
        {
            get
            {
                return this;
            }
        }
        public void CopyTo(Array array, int index)
        {
            for (IEnumerator e = GetEnumerator(); e.MoveNext(); )
                array.SetValue(e.Current, index++);
        }
        public IEnumerator GetEnumerator()
        {
            return new BasicDataSourceEnumerator(dataItemCount);
        }
    }
}
