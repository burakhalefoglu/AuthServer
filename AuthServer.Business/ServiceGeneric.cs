using AuthServer.Core.DataAccess;
using AuthServer.Core.Service;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Business
{
    public class ServiceGeneric<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<TEntity> _entityRepository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IEntityRepository<TEntity> entityRepository)
        {
            _unitOfWork = unitOfWork;
            _entityRepository = entityRepository;
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _entityRepository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return ResponseDto<TDto>.Success(newDto, 200);
        }

        public Task<ResponseDto<NoDataContent>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _entityRepository.GetAllAsync());

            return ResponseDto<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var product = await _entityRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ResponseDto<TDto>.Fail("Id not found", 404, true);
            }

            return ResponseDto<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<ResponseDto<NoDataContent>> Remove(int id)
        {
            var isExistEntity = await _entityRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return ResponseDto<NoDataContent>.Fail("Id not found", 404, true);
            }

            _entityRepository.Delete(isExistEntity);

            await _unitOfWork.CommitAsync();
            //204 durum kodu =>  No Content  => ResponseDto body'sinde hiç bir dat  olmayacak.
            return ResponseDto<NoDataContent>.Success(204);
        }

        public async Task<ResponseDto<NoDataContent>> UpdateAsync(TDto dto, int id)
        {
            var isExistEntity = await _entityRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return ResponseDto<NoDataContent>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(dto);

            _entityRepository.Update(updateEntity);

            await _unitOfWork.CommitAsync();
            return ResponseDto<NoDataContent>.Success(204);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            // where(x=>x.id>5)
            var list = _entityRepository.Where(predicate);

            return ResponseDto<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }


    }
}
