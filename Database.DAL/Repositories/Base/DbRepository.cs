﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.DAL.Context;
using Database.DAL.Entities.Base;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories.Base
{
    class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly WriteMeDatabase Database;
        private readonly DbSet<T> Set;
        public bool AutoSaveChanges { get; set; } = true;
        public DbRepository(WriteMeDatabase db)
        {
            Database = db;
            Set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => Set;
        public T Get(int id) => Items.SingleOrDefault(item => item.Id.Equals(id));

        public async Task<T> GetAsync(int id, CancellationToken cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.Id.Equals(id), cancel)
            .ConfigureAwait(false);

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            Database.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                Database.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            Database.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await Database.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            Database.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                Database.SaveChanges();
        }

        public Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
