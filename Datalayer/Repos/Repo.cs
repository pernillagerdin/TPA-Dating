using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Datalayer.Repos
{
    public abstract class Repo<TValue, TKey> where TValue : class
    {
        private ApplicationDbContext context;

        public Repo(ApplicationDbContext context) { this.context = context; }

        public DbSet<TValue> Items => context.Set<TValue>();
        public TValue Get(TKey Id) { return Items.Find(Id); }
        public List<TValue> GetAll() { return Items.ToList(); }
        public void Add(TValue item) { Items.Add(item); }
        public void Remove(TKey Id) { TValue item = Get(Id); Items.Remove(item); }
        public void Edit(TValue item) { context.Set<TValue>().AddOrUpdate(item); }
        public void Save() { context.SaveChanges(); }
    }
   
}
