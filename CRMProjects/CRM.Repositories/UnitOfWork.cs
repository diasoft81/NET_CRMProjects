using CRM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository _users;
        public IUserRepository Users => this._users;
        private bool _disposed = false;

        public UnitOfWork(AppDbContext context, IUserRepository users)
        {
            _users = users;
            _context = context;
        }

        public async Task DbSaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
