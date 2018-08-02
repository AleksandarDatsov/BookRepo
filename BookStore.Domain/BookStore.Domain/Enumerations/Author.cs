using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BookStore.Domain.Infrastructure.Repository.Interfaces;

namespace BookStore.Domain.Enumerations
{
    public class Author
    {
        private static IList<Author> authors;

        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public static ICollection<Author> GetAll()
        {
            if (authors == null)
            {
                throw new InvalidOperationException("Authors have not been initialized.");
            }

            return new ReadOnlyCollection<Author>(authors);
        }

        public static Author GetById(int id)
        {
            return GetAll().SingleOrDefault(p => p.Id == id);
        }

        public static void Initialize(IAuthorRepository repository)
        {
            authors = repository.GetAll().ToList();
        }

        public string FullName {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}