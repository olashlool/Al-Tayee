using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace Admin.Models.Services
{
    public class TypesRepo : ITypes
    {
        private readonly AltayeeDBContext _context;
        public TypesRepo(AltayeeDBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateType(Types Type)
        {
            int result;
            _context.Entry(Type).State = EntityState.Added;
            result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteType(Guid id)
        {
            Types oldCategory = await GetTypeById(id);
            _context.Entry(oldCategory).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Types> GetTypeById(Guid id)
        {
            return await _context.Types.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Types>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<int> UpdateType(Guid id, Types category)
        {
            int result;
            var updateTypes = new Types
            {
                Id = category.Id,
                ProductsId = category.ProductsId,
                Value = category.Value,
            };
            _context.Entry(updateTypes).State = EntityState.Modified;
            result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<Types>> GetTypeByProductId(Guid ProductId)
        {
            return await _context.Types.Where(x => x.ProductsId == ProductId).ToListAsync();
        }
    }
}
