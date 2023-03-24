

using InlamningDatabase2.Context;
using InlamningDatabase2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InlamningDatabase2.Services
{
    internal class CaseService
    {
        private readonly DataContext _context = new();
        private readonly UserService _userService = new();

        public async Task CreateAsync(CaseEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CaseEntity>> GetAllActiveAsync()
        {
            return await _context.Cases
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Include(x => x.Status)
                .Where(x=> x.StatusId != 3)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<CaseEntity>> GetAllAsync()
        {
            return await _context.Cases
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Include(x => x.Status)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
        {
            var _entity = await _context.Cases
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(predicate);

            return _entity!;
        }

        public async Task<CaseEntity?> GetById(int caseId)
        {
            var activcase = await _context.Cases.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == caseId);
            
            return activcase;
        }

        public async Task UpdatCaseStatusAsync(int caseId, int statusId)
        {
            var _entity = await _context.Cases.FindAsync(caseId);
            if (_entity != null)
            {
                _entity.Modified = DateTime.Now;
                _entity.StatusId = statusId;
                _context.Update(_entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCommentAsync(CommentEntity comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            await UpdatCaseStatusAsync(comment.CaseId, 2);
        }


    }
}
