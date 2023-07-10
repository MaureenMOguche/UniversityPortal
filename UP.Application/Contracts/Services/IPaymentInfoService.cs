using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Contracts.Persistence;
using UP.Application.Models;
using UP.Domain;

namespace UP.Application.Contracts.Services
{
    public interface IPaymentInfoService
    {
        Task<APIResponse> GetAllAsync(string? includeproperties = null, Expression<Func<PaymentInfo, bool>>? filter = null);
        Task<APIResponse> GetOneAsync(Expression<Func<PaymentInfo, bool>> filter, string? includeproperties = null);
        Task<APIResponse> CreateAsync(PaymentInfo entity);
        Task<APIResponse> UpdateAsync(PaymentInfo entity);
        Task<APIResponse> DeleteAsync(PaymentInfo entity);
        Task<APIResponse> BulkDeleteAsync(IEnumerable<PaymentInfo> entities);
    }
}
