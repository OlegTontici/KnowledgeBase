using Dapper;
using System.Data.SqlClient;

namespace KnowledgeBase.Persistence.Sql
{
    public static class KnowledgeBaseDataContext
    {
        private static string _connectionString = "Server=localhost;Integrated Security=true;MultipleActiveResultSets=true";

        public static void Setup()
        {
            var createDbScript = @"
            IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KnowledgeBase')
            CREATE DATABASE KnowledgeBase;";

            var createTablesScript = @"
            USE KnowledgeBase

            IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id ('[KnowledgeBase].[dbo].[Tags]')) 
            CREATE TABLE [KnowledgeBase].[dbo].[Tags] (
                Value nvarchar(450) NOT NULL,
                DateAdded datetime2(7) NOT NULL,
                PRIMARY KEY (Value)
            )

            IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id ('[KnowledgeBase].[dbo].[SmartThoughts]')) 
            CREATE TABLE [dbo].[SmartThoughts] (
                Id uniqueidentifier NOT NULL,
                Title nvarchar(MAX) NOT NULL,
                FormattedContent nvarchar(MAX) NOT NULL,
                Preview nvarchar(MAX) NOT NULL,
                DateAdded datetime2(7) NOT NULL,
                Tags nvarchar(450) NOT NULL,
                PRIMARY KEY (Id)
            )

            IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IDX_Tags' AND object_id = OBJECT_ID('[KnowledgeBase].[dbo].[SmartThoughts]'))
            CREATE INDEX IDX_Tags
            ON [KnowledgeBase].[dbo].[SmartThoughts] (Tags)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(createDbScript);
                connection.Execute(createTablesScript);
            }
        }
    }
}
