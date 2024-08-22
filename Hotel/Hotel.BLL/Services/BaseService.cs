using Hotel.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
        }
    }
}
