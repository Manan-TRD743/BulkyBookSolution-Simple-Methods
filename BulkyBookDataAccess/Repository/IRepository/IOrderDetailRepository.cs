using BulkyBookModel;
using BulkyBookSolution.BulkyBookModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : Irepository<OrderDetailModel>
    {
        //Declaration Of Methos for Update orderDetail 
        void Update(OrderDetailModel orderDetail);
    }
}
