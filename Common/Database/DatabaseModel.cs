using System;
using Common.Database.Tables;
using Common.Globals;
using Shaolinq;
using Shaolinq.MySql;

namespace Common.Database
{
    public class DatabaseModel<T> where T : DataAccessModel
    {
        protected T model;

        public DatabaseModel()
        {
            //var configuration = SqliteConfiguration.Create("database.sqlite", null);
            var configuration = MySqlConfiguration.Create("wow", "127.0.0.1", "homestead", "secret");

            try
            {
                model = DataAccessModel.BuildDataAccessModel<T>(configuration);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    [DataAccessModel]
    public abstract class Models : DataAccessModel
    {
        [DataAccessObjects]
        public abstract DataAccessObjects<Users> Users { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<Realms> Realms { get; }
    }

    public class DatabaseManager : DatabaseModel<Models>
    {
        public DatabaseManager()
        {
            //if (File.Exists("database.sqlite"))
                //return;

            // Recria a base inteira
            model.Create(DatabaseCreationOptions.DeleteExistingDatabase);

            using (var scope = new DataAccessScope())
            {
                // Inserindo Usuarios
                var User = model.Users.Create();
                User.name       = "John Doe";
                User.username   = "john";
                User.email      = "john@doe.com";
                User.password   = "doe";
                User.created_at = DateTime.Now;

                var User2 = model.Users.Create();
                User2.name       = "Dabal Doe";
                User2.username   = "doe";
                User2.email      = "dabal@doe.com";
                User2.password   = "doe";
                User2.created_at = DateTime.Now;

                var User3 = model.Users.Create();
                User3.name       = "John Doe";
                User3.username   = "ban";
                User3.email      = "john@doe.com";
                User3.password   = "doe";
                User3.created_at = DateTime.Now;
                User3.bannet_at  = DateTime.Now;

                // Inserindo Realm
                var RealmPVP = model.Realms.Create();
                RealmPVP.flag       = RealmFlag.NewPlayers;
                RealmPVP.timezone   = RealmTimezone.AnyLocale;
                RealmPVP.type       = RealmType.PVP;
                RealmPVP.name       = "Firetree";
                RealmPVP.address    = "127.0.0.1:1001";
                RealmPVP.created_at = DateTime.Now;

                var RealmPVE = model.Realms.Create();
                RealmPVE.flag       = RealmFlag.NewPlayers;
                RealmPVE.timezone   = RealmTimezone.AnyLocale;
                RealmPVE.type       = RealmType.Normal;
                RealmPVE.name       = "Quel'Thalas";
                RealmPVE.address    = "127.0.0.1:1002";
                RealmPVE.created_at = DateTime.Now;

                scope.Complete();
            }
        }
    }
}
