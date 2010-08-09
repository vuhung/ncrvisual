using System;

namespace NCRVisual.DataProvider
{
    public class DefaultDataProvider : IDataProvider
    {

        #region IDataProvider Members

        public int[][] GetGaphMatrix()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Entities.Message> GetMessageList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
