using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Data.Repositories;
using RealtimeSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Service
{
    public interface IDevTestService
    {
        void Add(DevTest devTest);
        IEnumerable<DevTest> GetAll();
        void Update(DevTest devTest);
        void Delete(DevTest devTest);
        DevTest GetById(int Id);
        IEnumerable<DevTest> GetAllAfterTime(Expression<Func<DevTest, bool>> where);
        void Save();
    }
    public class DevTestService : IDevTestService
    {
        private readonly IDevTestRepository _devTestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DevTestService(IDevTestRepository devTestRepository, IUnitOfWork unitOfWork)
        {
            _devTestRepository = devTestRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(DevTest devTest)
        {
            _devTestRepository.Add(devTest);
        }

        public void Delete(DevTest devTest)
        {
            _devTestRepository.Delete(devTest);
        }

        public void Save()
        {
            _unitOfWork.commit();
        }

        public void Update(DevTest devTest)
        {
            _devTestRepository.Update(devTest);
        }
        public IEnumerable<DevTest> GetAll()
        {
           return _devTestRepository.GetAll();
        }
        public DevTest GetById(int Id)
        {
            return _devTestRepository.GetById(Id);
        }
        public IEnumerable<DevTest> GetAllAfterTime(Expression<Func<DevTest, bool>> where)
        {
            return _devTestRepository.GetAllAfterTime(where);
        }
        public IEnumerable<DevTest> GetAllDeleted(Expression<Func<DevTest, bool>> where)
        {
            return _devTestRepository.GetAllDeleted(where);
        }
        
    }
}
