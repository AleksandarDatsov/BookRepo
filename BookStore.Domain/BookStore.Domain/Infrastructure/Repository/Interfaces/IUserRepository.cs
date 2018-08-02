using System;
using System.Linq.Expressions;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        User Find(string username);

        User SingleOrDefault(Expression<Func<User, bool>> predicate);

        void Add(User user);
    }
}