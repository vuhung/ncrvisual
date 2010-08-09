using System.Collections.Generic;
using NCRVisual.Entities;

namespace NCRVisual.DataProvider
{
    public interface IDataProvider
    {
        int[][] GetGaphMatrix();
        List<Message> GetMessageList();
    }
}
