using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Service
{
    public interface IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<ResponseDto<NoDataContent>> Delete(TEntity entity);
        Task<ResponseDto<NoDataContent>> UpdateAsync(TEntity entity);
    }
}
