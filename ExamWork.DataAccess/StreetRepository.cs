using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ExamWork.DataAccess
{
    public class StreetRepository : IRepository<Street>
    {
        private DbConnection _connection;
        private Migration _migration;

        public StreetRepository(DbConnection connection)
        {
            _connection = connection;
            _migration = new Migration();
        }

        public void Add(Street item)
        {
            _migration.CheckMigration();

            var sqlQuery = "insert into Streets (Id,CreationDate,DeletedDate,Name,CityId) values(@Id,@CreationDate,@DeletedDate,@Name,@CityId)";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Name, item.CityId });

            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            _migration.CheckMigration();

            var sqlQuery = "update Streets set DeletedDate = GetDate() where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1) throw new Exception("Запись не удалена!");
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Street> GetAll()
        {
            var sqlQuery = "select * from Streets";
            return _connection.Query<Street>(sqlQuery).AsList();
        }

        public void Update(Street item)
        {
            _migration.CheckMigration();

            var sqlQuery = "update Streets set CreationDate = @CreationDate, DeletedDate = @DeletedDate, Name = @Name, CityId=@CityId where Id = @Id";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Name, item.CityId });

            if (result < 1) throw new Exception("Запись не обновлена!");
        }
    }
}
