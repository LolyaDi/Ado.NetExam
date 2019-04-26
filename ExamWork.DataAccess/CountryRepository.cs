using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ExamWork.DataAccess
{
    public class CountryRepository : IRepository<Country>
    {
        private DbConnection _connection;
        private Migration _migration;

        public CountryRepository(DbConnection connection)
        {
            _connection = connection;
            _migration = new Migration();
        }

        public void Add(Country item)
        {
            _migration.CheckMigration();

            var sqlQuery = "insert into Countries (Id,CreationDate,DeletedDate,Name) values(@Id,@CreationDate,@DeletedDate,@Name)";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Name });

            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            _migration.CheckMigration();

            var sqlQuery = "update Countries set DeletedDate = GetDate() where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1) throw new Exception("Запись не удалена!");
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Country> GetAll()
        {
            var sqlQuery = "select * from Countries";
            return _connection.Query<Country>(sqlQuery).AsList();
        }

        public void Update(Country item)
        {
            _migration.CheckMigration();

            var sqlQuery = "update Countries set CreationDate = @CreationDate, DeletedDate = @DeletedDate, Name = @Name where Id = @Id";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Name });

            if (result < 1) throw new Exception("Запись не обновлена!");
        }
    }
}
