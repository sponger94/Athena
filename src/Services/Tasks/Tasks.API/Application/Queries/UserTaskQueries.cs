using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tasks.API.Application.Queries
{
    public class UserTaskQueries
        : IUserTaskQueries
    {
        private readonly string _connectionString;

        #region SqlQueries

          private const string GetUserTaskByIdSqlQuery = @"
                        SELECT TOP(1)
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        WHERE t.[Id] = @userTaskId;

                        SELECT
                            ats.[Uri] AS uri
                        FROM [tasks].[attachments] AS ats
                        WHERE ats.[UserTaskId] = @userTaskId;

                        SELECT
                            l.[Id] AS labelid,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname
                        FROM [tasks].[labels] AS l
                        INNER JOIN [tasks].[labelItems] AS li ON l.[Id] = li.[LabelId]
                        WHERE li.[UserTaskId] = @userTaskId;

                        SELECT
                            n.[Content]
                        FROM [tasks].[notes] AS n
                        WHERE n.[UserTaskId] = @userTaskId;

                        SELECT
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted
                        FROM [tasks].[subTasks] AS st
                        WHERE st.[UserTaskId] = @userTaskId";

        private const string GetUserTasksSqlQuery = @"
                        SELECT
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId
                        ORDER BY t.[DateCreated]
                        OFFSET @offset ROWS
                        FETCH NEXT @pageSize ROWS ONLY";

        private const string GetProjectUserTasksSqlQuery = @"
                        SELECT
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId AND p.[Id] = @projectId
                        ORDER BY t.[DateCreated]
                        OFFSET @offset ROWS
                        FETCH NEXT @pageSize ROWS ONLY";

        #endregion
      
        public UserTaskQueries(string connString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connString)
                ? connString
                : throw new ArgumentNullException(nameof(connString));
        }

        public async Task<UserTask> GetTaskAsync(int userTaskId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiResult = await connection.QueryMultipleAsync(GetUserTaskByIdSqlQuery, new { id = userTaskId });

                var taskEntry = await multiResult.ReadSingleAsync<UserTask>();
                taskEntry.attachments.AddRange(await multiResult.ReadAsync<Attachment>());
                taskEntry.labels.AddRange(await multiResult.ReadAsync<Label>());
                taskEntry.notes.AddRange(await multiResult.ReadAsync<Note>());
                taskEntry.subtasks.AddRange(await multiResult.ReadAsync<SubTask>());

                return taskEntry;
            }
        }

        public async Task<IEnumerable<UserTaskSummary>> GetUserTasksAsync(Guid userId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var offset = pageSize * pageIndex;
                var multiResult = await connection.QueryMultipleAsync(GetUserTasksSqlQuery, 
                    new { userId, offset, pageSize });

                return await multiResult.ReadAsync<UserTaskSummary>();
            }
        }

        public async Task<IEnumerable<UserTaskSummary>> GetProjectUserTasksAsync(Guid userId, int projectId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var offset = pageSize * pageIndex;
                var multiResult = await connection.QueryMultipleAsync(GetProjectUserTasksSqlQuery, 
                    new { userId, projectId, offset, pageSize });

                return await multiResult.ReadAsync<UserTaskSummary>();
            }
        }
    }
}
