using MeterReadings.Database.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace MeterReadings.Database.Sqlite
{
    public class SqliteRepository : IRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public SqliteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("MeterReadings");

            InitialiseDatabase();
        }

        public List<Account> GetAccounts()
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            using var cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT AccountId, FirstName, LastName FROM Account";
            DbDataReader reader = cmd.ExecuteReader();

            return ToAccounts(reader);
        }

        public List<MeterReading> GetMeterReadings()
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            using var cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT AccountId, MeterReadingDateTime, MeterReadValue FROM MeterReading";
            DbDataReader reader = cmd.ExecuteReader();

            return ToMeterReadings(reader);
        }

        public void SaveMeterReadings(List<MeterReading> meterReadings)
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            using var cmd = new SQLiteCommand();
            cmd.Connection = con;

            foreach (MeterReading meterReading in meterReadings)
            {
                cmd.CommandText = "INSERT INTO MeterReading (AccountId, MeterReadingDateTime, MeterReadValue) VALUES (@AccountId, @MeterReadingDateTime, @MeterReadValue)";
                cmd.Parameters.AddWithValue("@AccountId", meterReading.AccountId);
                cmd.Parameters.AddWithValue("@MeterReadingDateTime", meterReading.MeterReadingDateTime);
                cmd.Parameters.AddWithValue("@MeterReadValue", meterReading.MeterReadingValue);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        private List<Account> ToAccounts(DbDataReader reader)
        {
            List<Account> accounts = new List<Account>();

            while (reader.Read())
            {
                Account account = new Account();
                account.AccountId = int.Parse(reader["AccountId"].ToString());
                account.FirstName = reader["FirstName"].ToString();
                account.LastName = reader["LastName"].ToString();
                accounts.Add(account);
            }

            return accounts;
        }

        private List<MeterReading> ToMeterReadings(DbDataReader reader)
        {
            List<MeterReading> meterReadings = new List<MeterReading>();

            while (reader.Read())
            {
                MeterReading meterReading = new MeterReading();
                meterReading.AccountId = int.Parse(reader["AccountId"].ToString());
                meterReading.MeterReadingDateTime = DateTime.Parse(reader["MeterReadingDateTime"].ToString());
                meterReading.MeterReadingValue = int.Parse(reader["MeterReadValue"].ToString());
                meterReadings.Add(meterReading);
            }

            return meterReadings;
        }

        private void InitialiseDatabase()
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            using var cmd = new SQLiteCommand();
            cmd.Connection = con;

            cmd.CommandText = "DROP TABLE IF EXISTS MeterReading";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE MeterReading (AccountId INTEGER, MeterReadingDateTime DATETIME, MeterReadValue INTEGER)";
            cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO MeterReading (AccountId, MeterReadingDateTime, MeterReadValue) VALUES (2344, '2019-04-22 09:24:00', 123)";
            //cmd.ExecuteScalar();

            cmd.CommandText = "DROP TABLE IF EXISTS Account";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE Account (AccountId INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2344, 'Tommy', 'Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2233,'Barry','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (8766,'Sally','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2345,'Jerry','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2346,'Ollie','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2347,'Tara','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2348,'Tammy','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2349,'Simon','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2350,'Colin','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2351,'Gladys','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2352,'Greg','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2353,'Tony','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2355,'Arthur','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (2356,'Craig','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (6776,'Laura','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (4534,'JOSH','TEST')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1234,'Freya','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1239,'Noddy','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1240,'Archie','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1241,'Lara','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1242,'Tim','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1243,'Graham','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1244,'Tony','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1245,'Neville','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1246,'Jo','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1247,'Jim','Test')";
            cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO Account (AccountId, FirstName, LastName) VALUES (1248,'Pam','Test')";
            cmd.ExecuteScalar();
        }
    }
}
