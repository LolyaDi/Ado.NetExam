using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ExamWork.DataAccess
{
    public class UnitOfWork: IDisposable
    {
        private DbConnection _connection;
        private CountryRepository _countryRepository;
        private CityRepository _cityRepository;
        private StreetRepository _streetRepository;

        public UnitOfWork()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;

            _connection = new SqlConnection(connectionString);
        }

        public CountryRepository Countries
        {
            get
            {
                if (_countryRepository == null)
                    _countryRepository = new CountryRepository(_connection);
                return _countryRepository;
            }
        }

        public CityRepository Cities
        {
            get
            {
                if (_cityRepository == null)
                    _cityRepository = new CityRepository(_connection);
                return _cityRepository;
            }
        }

        public StreetRepository Streets
        {
            get
            {
                if (_streetRepository == null)
                    _streetRepository = new StreetRepository(_connection);
                return _streetRepository;
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
