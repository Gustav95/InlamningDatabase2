

using InlamningDatabase2.Context;
using InlamningDatabase2.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace InlamningDatabase2.Services
{
    internal class StatusService
    {
        private readonly DataContext _context = new();
        public async Task CreateStatusTypesAsync()
        {
            if (!await _context.Statuses.AnyAsync())
            {
                string[] _statuses = new string[] { "Ej Börjad", "Pågår", "Avslutad" };

                foreach(var status in _statuses)
                {
                    await _context.AddAsync(new StatusEntity { StatusName = status });
                    await _context.SaveChangesAsync();
                }
            }
        }
        
    }
}
