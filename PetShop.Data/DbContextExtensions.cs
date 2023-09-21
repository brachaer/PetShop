using Microsoft.EntityFrameworkCore;

namespace PetShop.Data
{
    public static class DbContextExtensions
    {
        private const string SetIdentityInsert = "SET IDENTITY_INSERT";

        public static void EnableIdentityInsert(this DbContext context, Type entityType)
        {
            var entityName = context.Model.FindEntityType(entityType)?.GetTableName();
            if (string.IsNullOrEmpty(entityName))
                return;
            context.Database.ExecuteSqlRaw($"{SetIdentityInsert} {entityName} ON");
        }
        public static void EnableIdentityInsert<T>(this DbContext context) where T : class
        {
            context.EnableIdentityInsert(typeof(T));
        }
        public static void DisableIdentityInsert(this DbContext context, Type entityType)
        {
            var entityName = context.Model.FindEntityType(entityType)?.GetTableName();
            if (string.IsNullOrEmpty(entityName))
                return;
            context.Database.ExecuteSqlRaw($"{SetIdentityInsert} {entityName} OFF");
        }
        public static void DisableIdentityInsert<T>(this DbContext context) where T : class
        {

            context.DisableIdentityInsert(typeof(T));
        }

        public static void SaveChangesWithIdentityInsert(this DbContext context, params Type[] types)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var type in types)
                {
                    context.EnableIdentityInsert(type);
                }
                context.SaveChanges();

                foreach (var type in types)
                {
                    context.DisableIdentityInsert(type);
                }
                transaction.Commit();
            }
        }

        public static void SaveChangesWithIdentityInsert<T>(this DbContext context) where T : class
        {
            context.SaveChangesWithIdentityInsert(typeof(T));
        }

        public static int GetIdentityForNewEntry<T>(this DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            if (entityType == null)
                return 0;

            var key = entityType.FindPrimaryKey();
            if (key == null || key.GetKeyType() != typeof(int))
                return 0;

            var tableName = entityType.GetTableName();
            var colName = key.Properties.First().Name;
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"SELECT TOP 1 {colName} FROM {tableName} ORDER BY {colName} DESC";

                context.Database.OpenConnection();
                var lastId = (int)command.ExecuteScalar();
                context.Database.CloseConnection();
                return ++lastId;
            }
        }
    }
}
