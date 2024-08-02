using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IDal;
using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Data.Entites;
using PLManagementSystem.Data.Repository;

namespace PLManagementSystem.Data.Wrapper
{
    public class DataWrapper : IDataWrapper
    {
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Day> _dayRepository;
        private IGenericRepository<Class> _classRepository;
        private IGenericRepository<LessonGroups> _lessonGroupsRepository;
        public DataWrapper(AppDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork(_context);
                }
                return _unitOfWork;
            }
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<GenericRepository<User>>>();
                    _userRepository = new GenericRepository<User>(_context, logger);
                }
                return _userRepository;
            }
        }
        public IGenericRepository<Day> DayRepository
        {
            get
            {
                if (_dayRepository == null)
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<GenericRepository<Day>>>();
                    _dayRepository = new GenericRepository<Day>(_context, logger);
                }
                return _dayRepository;
            }
        }
        public IGenericRepository<Class> ClassRepository
        {
            get
            {
                if (_classRepository == null)
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<GenericRepository<Class>>>();
                    _classRepository = new GenericRepository<Class>(_context, logger);
                }
                return _classRepository;
            }
        }
        public IGenericRepository<LessonGroups> LessonGroupsRepository
        {
            get
            {
                if (_lessonGroupsRepository == null)
                {
                    var logger = _serviceProvider.GetRequiredService<ILogger<GenericRepository<LessonGroups>>>();
                    _lessonGroupsRepository = new GenericRepository<LessonGroups>(_context, logger);
                }
                return _lessonGroupsRepository;
            }
        }
    }
}
