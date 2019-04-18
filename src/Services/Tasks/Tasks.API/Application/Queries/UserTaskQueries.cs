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

        private const string GetTaskByIdSqlQuery = @"
                        SELECT TOP(1)
                            t.[Id] AS tasknumber,
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        WHERE t.[Id] = @id;

                        SELECT
                            ats.[Uri] AS uri
                        FROM [tasks].[attachments] AS ats
                        WHERE ats.[UserTaskId] = @id;

                        SELECT
                            l.[Id] AS labelid,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname
                        FROM [tasks].[labels] AS l
                        INNER JOIN [tasks].[labelItems] AS li ON l.[Id] = li.[LabelId]
                        WHERE li.[UserTaskId] = @id;

                        SELECT
                            n.[Content]
                        FROM [tasks].[notes] AS n
                        WHERE n.[UserTaskId] = @id;

                        SELECT
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted
                        FROM [tasks].[subTasks] AS st
                        WHERE st.[UserTaskId] = @id";

        private const string GetTasksForUserSqlQuery = @"
                        SELECT
                            t.[Id] AS tasknumber,
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId;

                        SELECT
                            ats.[Uri] AS uri
                        FROM [tasks].[attachments] AS ats
                        INNER JOIN [tasks].[userTasks] AS t ON ats.[UserTaskId] = t.[Id]
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId;

                        SELECT
                            l.[Id] AS labelid,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname
                        FROM [tasks].[labels] AS l
                        INNER JOIN [tasks].[labelItems] AS li ON l.[Id] = li.[LabelId]
                        INNER JOIN [tasks].[userTasks] AS t ON li.[UserTaskId] = t.[Id]
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId;

                        SELECT
                            n.[Content]
                        FROM [tasks].[notes] AS n
                        INNER JOIN [tasks].[userTasks] AS t ON n.[UserTaskId] = t.[Id]
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId;

                        SELECT
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted
                        FROM [tasks].[subTasks] AS st
                        INNER JOIN [tasks].[userTasks] AS t ON st.[UserTaskId] = t.[Id]
                        INNER JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        WHERE p.[IdentityGuid] = @userId";

        public UserTaskQueries(string connString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connString)
                ? connString
                : throw new ArgumentNullException(nameof(connString));
        }

        public async Task<UserTask> GetTaskAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiResult = await connection.QueryMultipleAsync(GetTaskByIdSqlQuery, new { id });

                var taskEntry = await multiResult.ReadSingleAsync<UserTask>();
                taskEntry.attachments.AddRange(await multiResult.ReadAsync<Attachment>());
                taskEntry.labels.AddRange(await multiResult.ReadAsync<Label>());
                taskEntry.notes.AddRange(await multiResult.ReadAsync<Note>());
                taskEntry.subtasks.AddRange(await multiResult.ReadAsync<SubTask>());

                return taskEntry;
            }
        }

        public async Task<IEnumerable<UserTask>> GetTasksFromUserAsync(Guid userId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiResult = await connection.QueryMultipleAsync(GetTaskByIdSqlQuery, new { userId });

                var tasks = await multiResult.ReadAsync<Task>();
                taskEntry.attachments.AddRange(await multiResult.ReadAsync<Attachment>());
                taskEntry.labels.AddRange(await multiResult.ReadAsync<Label>());
                taskEntry.notes.AddRange(await multiResult.ReadAsync<Note>());
                taskEntry.subtasks.AddRange(await multiResult.ReadAsync<SubTask>());

                return taskEntry;
            }
        }
    }
}
