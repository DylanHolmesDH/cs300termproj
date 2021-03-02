using ChocAnDatabase.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnDatabase
{
    public interface IDatabaseWrapper
    {
        MemberRecord FetchMember(int id);
    }
}
