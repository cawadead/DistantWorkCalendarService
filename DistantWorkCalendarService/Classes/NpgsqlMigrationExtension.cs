using Microsoft.EntityFrameworkCore;

namespace DistantWorkCalendarService.Classes
{
    public class NpgsqlMigrationExtension
    {
        public static async Task BeginMigrationsAsync<T>(string connectionString) where T : DbContext
        {
            try
            {
                DbContextOptionsBuilder<T> optionBuilder = new DbContextOptionsBuilder<T>()
                                                        .UseNpgsql(connectionString);

                T metadataContext = (T)Activator.CreateInstance(typeof(T), optionBuilder.Options);

                metadataContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));

                Console.WriteLine("Migration Start");

                await metadataContext.Database.MigrateAsync();

                if (metadataContext.Database.IsNpgsql())
                {
                    // сбрасывает счетчики текущего значения инкрементируемого значения всех таблиц  (column id) (identity)
                    await metadataContext.Database.ExecuteSqlRawAsync(@"
                           DO $$
                                         BEGIN
                                               DECLARE nbRow int;
                                               DECLARE
                                                     tables CURSOR FOR
                                                           SELECT 'SELECT SETVAL(' ||
                                                        quote_literal(quote_ident(PGT.schemaname) || '.' || quote_ident(S.relname)) ||

                                                        ', COALESCE(MAX(' ||quote_ident(C.attname)|| '), 1) ) FROM ' ||
                                                        quote_ident(PGT.schemaname)|| '.'||quote_ident(T.relname)|| ';' AS val
                                                     FROM pg_class AS S,
                                                           pg_depend AS D,
                                                           pg_class AS T,
                                                           pg_attribute AS C,
                                                           pg_tables AS PGT
                                                     WHERE S.relkind = 'S'
                                                           AND S.oid = D.objid
                                                           AND D.refobjid = T.oid
                                                           AND D.refobjid = C.attrelid
                                                           AND D.refobjsubid = C.attnum
                                                           AND T.relname = PGT.tablename;
                                               BEGIN
                                                     FOR table_record IN tables LOOP
                                                     BEGIN
                                                           EXECUTE table_record.val INTO nbRow;
                                                           EXCEPTION WHEN OTHERS THEN
                                                     END;
                                                     END LOOP;
                                         EXCEPTION WHEN OTHERS THEN
                                         END;
                                         END$$;");
                }
                metadataContext.SaveChanges();
                Console.WriteLine("Migration End");
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!!!!!Migration no success!!!!!!!");
                Console.WriteLine("!!!!!!!Migration no success!!!!!!!");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("!!!!!!!Migration no success!!!!!!!");
                Console.WriteLine("!!!!!!!Migration no success!!!!!!!");
            }
        }
    }
}
