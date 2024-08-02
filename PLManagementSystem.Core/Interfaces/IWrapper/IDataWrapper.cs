using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Interfaces.IWrapper
{
    public interface IDataWrapper
    {
        public IUnitOfWork UnitOfWork { get; }
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Day> DayRepository { get; }
        public IGenericRepository<Class> ClassRepository { get; }
        public IGenericRepository<LessonGroups> LessonGroupsRepository { get; }

    }
}
