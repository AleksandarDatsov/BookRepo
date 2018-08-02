using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooksByAuthor(int authorId);

        IEnumerable<Book> GetAll();

        IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate);

        void Add(Book book);

        void Remove(Book entity);
    }
}