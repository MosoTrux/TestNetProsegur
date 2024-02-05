using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using TestNetProsegur.Infrastructure.DBContexts;

namespace TestNetProsegur.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _currentTransaction;

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IRepository<Order> OrderRepository => _orderRepository ?? new BaseRepository<Order>(_context);
        public IRepository<Product> ProductRepository => _productRepository ?? new BaseRepository<Product>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            if (_currentTransaction is not null)
            {
                throw new InvalidOperationException("La transacción ya está en curso.");
            }

            //_currentTransaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.SaveChanges();
                //_currentTransaction?.Commit();
            }
            catch
            {
                _currentTransaction?.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            if (_currentTransaction is not null)
            {
                _currentTransaction.Rollback();
                _currentTransaction.Dispose();
            }
        }
    }
}
