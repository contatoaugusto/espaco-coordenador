using System.Collections;

namespace EC.UI.WebControls
{
    public class BasicDataSourceEnumerator : IEnumerator
    {
        private int count;
        private int index;
        public BasicDataSourceEnumerator(int count)
        {
            this.count = count;
            index = -1;
        }
        public object Current
        {
            get
            {
                return null;
            }
        }
        public bool MoveNext()
        {
            index++;
            return index < count;
        }
        public void Reset()
        {
            index = -1;
        }
    }
}
