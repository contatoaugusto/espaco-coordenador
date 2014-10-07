using EC.Common;
namespace EC.Common
{
    public class ReturnObject<T>
    {
        public AlertList AlertList { get; private set; }
        public T Object { get; private set; }

        public void SetReturn(AlertList alertList, T returnObject)
        {
            AlertList = alertList;
            Object = returnObject;
        }
    }
}
