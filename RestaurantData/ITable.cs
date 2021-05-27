using RestaurantBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantBookingApi.RestaurantData
{
    public interface ITable
    {
        List<Table> GetTables();
        Table GetTable(int? id);
        Table AddTable(Table table);
        void DeleteTable(Table table);
        Table EditTable(Table table);
        bool IsTableExist(Table table);
    }
}
