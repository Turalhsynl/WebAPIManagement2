using Dal.SqlServer.Context;
using Dal.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace Dal.SqlServer.UnitOfWork
{
    public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
    {
        private readonly string _connectionString = connectionString;
        private readonly AppDbContext _context = context;

        public SqlBookRepository bookRepository;
        public SqlUserRepository userRepository;

        public IBookRepository BookRepository => bookRepository ?? new SqlBookRepository(_context);
        public IUserRepository UserRepository => userRepository ?? new SqlUserRepository(_connectionString ,_context);
    }
}
