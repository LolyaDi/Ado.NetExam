using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ExamWork.DataAccess
{
    public class CityRepository : IRepository<City>
    {
        private DbConnection _connection;
        private Migration _migration;

        public CityRepository(DbConnection connection)
        {
            _connection = connection;
            _migration = new Migration();
        }

        public void Add(City item)
        {
            _migration.CheckMigration();

            var sqlQuery = "insert into Cities (Id,CreationDate,DeletedDate,Name,CountryId) values(@Id,@CreationDate,@DeletedDate,@Name,@CountryId)";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Name, item.CountryId });

            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            _migration.CheckMigration();
            
            var sqlQuery = "update Cities set DeletedDate = GetDate() where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1) throw new Exception("Запись не удалена!");
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<City> GetAll()
        {
            var sqlQuery = "select * from Cities";
            return _connection.Query<City>(sqlQuery).AsList();
        }

        public void Update(City item)
        {
            _migration.CheckMigration();

            var sqlQuery = "update Cities set CreationDate = @CreationDate, DeletedDate = @DeletedDate, Name = @Name, CountryId = @CountryId where Id = @Id";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.CountryId });

            if (result < 1) throw new Exception("Запись не обновлена!");
        }
    }
}
