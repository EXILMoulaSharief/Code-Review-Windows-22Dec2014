using System;
using System.IO;
using System.Linq;
using TeacherApp.Client.UI.Cache;
namespace TeacherApp.Client.UI.WinApp.Cache
{
    internal class CacheExpiryManager : ICacheExpiryManager
    {
        private readonly string _databaseName;
        private readonly string _databaseFilePath;

        public CacheExpiryManager(string storeName)
        {
            if(string.IsNullOrWhiteSpace(storeName)) throw new ArgumentException("You must provide a cache store name.", "storeName");

            _databaseName = storeName;

            _databaseFilePath = GetDatabaseFilePath();
            if(_databaseFilePath!= string.Empty)
            {
                using(SQLiteConnection connection = GetConnection(_databaseFilePath))
                {
                    connection.CreateTable<CachedItem>();
                }
            }
        }

        public bool HasExpired(int studentId)
        {
            bool hasExpired = false;
            using(var connection = new SQLiteConnection(_databaseFilePath))
            {
                var cachedItem = connection.Query<CachedItem>("select * from CachedItems where StudentId = ?", studentId).SingleOrDefault();
                if(cachedItem != null)
                {
                    double totalDays = (DateTime.Now - cachedItem.ExpiryDate).TotalDays;
                    hasExpired = totalDays > 7.0d;
                }
            }

            return hasExpired;
        }

        public bool EntryExists(int studentId)
        {
            bool exists;
            using(var connection = new SQLiteConnection(_databaseFilePath))
            {
                var cachedItem = connection.Query<CachedItem>("select * from CachedItems where StudentId = ?", studentId).SingleOrDefault();

                exists = cachedItem != null;
            }
            return exists;
        }

        public void SetExpiration(int studentId, DateTime expirationDate)
        {
            using(var connection = new SQLiteConnection(_databaseFilePath))
            {
                var cachedItem = connection.Query<CachedItem>("select * from CachedItems where StudentId = ?", studentId).SingleOrDefault();
                if(cachedItem != null)
                {
                    cachedItem.ExpiryDate = DateTime.Now;
                    connection.Update(cachedItem);
                }
                else
                {
                    var itemToCache = new CachedItem();
                    itemToCache.StudentId = studentId;
                    itemToCache.ExpiryDate = DateTime.Now;
                    connection.Insert(itemToCache);
                }
            }
        }

        private string GetDatabaseFilePath()
        {
            if(string.IsNullOrWhiteSpace(_databaseName)) throw new InvalidOperationException("The Cache has not been initilized. Invoke the Initialize method before calling any other method on this class.");

            string databaseFileName = string.Format("{0}.db", _databaseName);
            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, databaseFileName);
            //if (Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(_databaseName).Status != Windows.Foundation.AsyncStatus.Error)
           // {
                //var documentFolderPath = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(databaseFileName);// Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                SQLite.SQLiteAsyncConnection con = new SQLite.SQLiteAsyncConnection(databaseFilePath);
                
           // }
           
            return databaseFilePath;
        }

        private SQLiteConnection GetConnection(string databaseFilePath)
        {
            return new SQLiteConnection(databaseFilePath);
        }

        [Table("CachedItems")]
        private class CachedItem
        {
            [PrimaryKey, Column("StudentId")]
            public int StudentId { get; set; }

            public DateTime ExpiryDate { get; set; }
        }
    }
}