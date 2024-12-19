using BarrierGateApi.Singleton;
using Microsoft.EntityFrameworkCore;
using BarrierGateApi.Models;

namespace BarrierGateApi.DB.Context
{
    public class Context<T> : DbContext where T : GestionnableElement
    {
        protected string sqlFilePath { get; set; }
        public Context(string sqliteFilePath) 
        {
            this.sqlFilePath = sqliteFilePath;
        }

        public virtual DbSet<T> context { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlite($"DataSource={this.sqlFilePath}");
            }
        }

        public async Task<List<T>> GetAll()
        {
            List<T> list = await this.context.ToListAsync();
            return list;
        }

        public async Task<T?> Get(int id) 
        {
            List<T> list = await this.context.ToListAsync();
            return list.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<bool> Add(T obj) 
        {
            try
            {
                this.context.Add(obj);
                await this.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> Add(List<T> objList)
        {
            try
            {
                foreach (T obj in objList) 
                {
                    this.context.Add(obj);
                }
                await this.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// This function Update an object with a given one updated and an ID
        /// </summary>
        /// <returns>
        /// If the Update is successfull the function will return a table with 2 index.
        /// [true, updated_object].
        /// 
        /// If the Update is failed the function will return a table with 1 index.
        /// [false]
        /// </returns>
        public async Task<object[]> Update(T obj) 
        {
            try
            {
                T objInDb = this.context.Find(obj.Id);
                objInDb = obj;
                await this.SaveChangesAsync();
                return [true, objInDb];
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return [false];
        }

        /// <summary>
        /// This function Update an object with a given one updated and an ID
        /// </summary>
        /// <returns>
        /// If the Update is successfull the function will return a table with 2 index.
        /// [true, list of updated_object].
        /// 
        /// If the Update is failed the function will return a table with 1 index.
        /// [false]
        /// </returns>
        public async Task<object[]> Update(List<T> objList)
        {
            try
            {
                foreach (T obj in objList)
                {
                    T objInDb = this.context.Find(obj.Id);
                    objInDb = obj;
                }
                await this.SaveChangesAsync();
                return [true, objList];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return [false];
        }

        public async Task<bool> Delete(T obj) 
        {
            try
            {
                this.context.Remove(obj);
                await this.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> Delete(List<T> objList)
        {
            try
            {
                foreach (T obj in objList)
                {
                    this.context.Remove(obj);
                }
                await this.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteAll() 
        {
            try
            {
                await this.context.ExecuteDeleteAsync();
                await this.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
