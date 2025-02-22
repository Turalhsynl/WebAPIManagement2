using Dal.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Dal.SqlServer.Infrastructure;

public class SqlBookRepository(AppDbContext context) : IBookRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }
    public void Update(Book book)
    {
        book.UpdatedDate = DateTime.Now;
        _context.Update(book);
    }
    public async Task Remove(int id, int deletedBy)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        book.IsDeleted = true;
        book.DeletedBy = deletedBy;
        book.DeletedDate = DateTime.Now;
    }
    public IQueryable<Book> GetAll()
    {
        return _context.Books;
    }
    public async Task<Book> GetByIdAsync(int id)
    {
        return (await _context.Books.FirstOrDefaultAsync(x => x.Id == id)!); ;
    }
}
