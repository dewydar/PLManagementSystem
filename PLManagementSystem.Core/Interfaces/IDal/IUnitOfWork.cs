using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Interfaces.IDal
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
