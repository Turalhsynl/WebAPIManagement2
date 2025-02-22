using Dal.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Dal.SqlServer.Infrastructure
{
    public class SqlUserRepository(string connectionString, AppDbContext appDbContext) : BaseSqlRepository(connectionString), IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public IQueryable<User> GetAll()
        {
            return _appDbContext.Users.OrderByDescending(c => c.CreatedBy).Where(c => c.IsDeleted == false);
        }

        public async Task<User> GetByEmailAsync(string email)
        {

            var sql = @"SELECT C.*
                        FROM Users AS C
                        WHERE C.Email = @email AND C.IsDeleted = 0";
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { email });
        }


        public async Task<User> GetByIdAsync(int id)
        {
            var sql = @"SELECT C.*
                        FROM Users AS C
                        WHERE C.Id = @id AND C.IsDeleted = 0";
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { id });
        }

        public async Task RegisterAsync(User user)
        {
            var sql = @"Insert INTO Users([Name],[Surname],[Email],[PasswordHash],
                        [Username],[FatherName],[Address],[MobilePhone],[CardNumber],[TableNumber],
                        [Birthdate],[DateOfEmployment],[DateOfDismissal],[Note],[Gender],[UserType],[CreatedBy])
                        VALUES (@Name, @Surname, @Email, @PasswordHash, @Username,
                                @FatherName, @Address, @MobilePhone, @CardNumber, @TableNumber,
                                @Birthdate, @DateOfEmployment,
                                @DateOfDismissal, @Note, @Gender,@UserType, @CreatedBy); SELECT SCOPE_IDENTITY()";
            using var conn = OpenConnection();
            var generatedId = await conn.ExecuteScalarAsync<int>(sql, user);
            user.Id = generatedId;
        }

        public bool Remove(int id, int deletedBy)
        {
            var checkSql = @"SELECT Id FROM Users WHERE Id = @id AND IsDeleted = 0";
            var sql = @"UPDATE Users
                SET IsDeleted = 1,
                    DeletedBy = @deletedBy,
                    DeletedDate = GETDATE()
                WHERE Id = @id";

            using var conn = OpenConnection();
            using var transaction = conn.BeginTransaction();

            var userId = conn.ExecuteScalar<int?>(checkSql, new { id }, transaction);

            if (!userId.HasValue)
                return false;

            var affectRows = conn.Execute(sql, new { id, deletedBy }, transaction);
            transaction.Commit();

            return affectRows > 0;
        }


        public void Update(User user)
        {
            var sql = @"UPDATE Users 
                        SET Name = @Name,
                        SET Surname = @Surname, 
                        SET Email = @Email,
                        SET PasswordHash = @PasswordHash,
                        SET Username = @Username,
                        SET FatherName = @FatherName,
                        SET Address = @Address,
                        SET MobilePhone = @MobilePhone,
                        SET CardNumber = @CardNumber,
                        SET TableNumber = @TableNumber,
                        SET Birthdate = @Birthdate,
                        SET DateOfEmployment = @DateOfEmployment,
                        SET DateOfDismissal = @DateOfDismissal,
                        SET Note = @Note,
                        SET Gender = @Gender,
                        SET UserType = @UserType,
                        SET UpdateBy = @UpdatedBy,
                        SET UpdatedDate = GETDATE()
                        SET WHERE Id = @Id";
            using var conn = OpenConnection();

            conn.Query(sql, user);
        }
    }
}
