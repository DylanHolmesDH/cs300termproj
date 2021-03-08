using ChocAnDatabase;
using ChocAnDatabase.records;
using System;

namespace ManagerTerminal {
    public class CrudServices {
        public void AddUpdateRemove(IDatabaseWrapper database, int optionNumber, Record record) {
            switch (optionNumber) {
                case 4:
                    database.AddMember(record);
                    database.Save();
                    break;
                case 5:
                    database.AddProvider(record);
                    database.Save();
                    break;
                default:
                    break;
            }
        }
    }
}