using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Tasks.API.Application.Queries
{
    public class LabelQueries : ILabelQueries
    {
        private readonly string _connectionString;

        public LabelQueries(string connString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connString)
                ? connString
                : throw new ArgumentNullException(nameof(connString));
        }

        #region SqlQueries

        private const string GetLabelByIdSqlQuery = @"SELECT * FROM [tasks].[labels] AS l WHERE l.Id = @labelId;";

        private const string GetLabelsSqlQuery = @"SELECT * FROM [tasks].[labels];";

        #endregion

        public async Task<Label> GetLabelByIdAsync(int labelId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiResult = await connection.QueryMultipleAsync(GetLabelByIdSqlQuery, new { id = labelId });
                return await multiResult.ReadSingleAsync<Label>();
            }
        }

        public async Task<IEnumerable<Label>> GetLabelsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiResult = await connection.QueryMultipleAsync(GetLabelsSqlQuery);
                return await multiResult.ReadAsync<Label>();
            }
        }
    }
}
