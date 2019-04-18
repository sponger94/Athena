using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.SeedWork;

namespace Tasks.API.Application.Queries
{
    public class UserTaskQueries
        : IUserTaskQueries
    {
        private readonly string _connectionString;
        
        private const string GetTaskByIdSqlQuery = @"SELECT 
                            t.[Id] AS tasknumber,
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted,
                            ats.[Uri] AS uri,
                            ats.[UserTaskId] as usertaskid,
                            l.[Id] AS labelid,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname,
                            n.[Content] AS content,
                            n.[UserTaskId] AS usertaskid,
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted,
                            st.[UserTaskId] AS usertaskid
                        FROM [tasks].[userTasks] AS t
                        LEFT JOIN [tasks].[attachments] AS ats ON t.[Id] = ats.[UserTaskId]
                        LEFT JOIN [tasks].[labelItems] AS li ON t.[Id] = li.[UserTaskId]
                        LEFT JOIN [tasks].[labels] AS l ON li.[LabelId] = l.[Id]
                        LEFT JOIN [tasks].[notes] AS n ON t.[Id] = n.[UserTaskId]
                        LEFT JOIN [tasks].[subTasks] AS st ON t.[Id] = st.[UserTaskId]
                        WHERE t.Id=@id";

        private const string GetTasksForUserSqlQuery = @"SELECT 
                            t.[Id] AS tasknumber,
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted,
                            ats.[Uri] AS uri,
                            ats.[UserTaskId] as usertaskid,
                            l.[Id] AS labelid,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname,
                            n.[Content] AS content,
                            n.[UserTaskId] AS usertaskid,
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted,
                            st.[UserTaskId] AS usertaskid
                        FROM [tasks].[userTasks] AS t
                        LEFT JOIN [tasks].[attachments] AS ats ON t.[Id] = ats.[UserTaskId]
                        LEFT JOIN [tasks].[labelItems] AS li ON t.[Id] = li.[UserTaskId]
                        LEFT JOIN [tasks].[labels] AS l ON li.[LabelId] = l.[Id]
                        LEFT JOIN [tasks].[notes] AS n ON t.[Id] = n.[UserTaskId]
						LEFT JOIN [tasks].[projects] AS p ON t.[ProjectId] = p.[Id]
                        LEFT JOIN [tasks].[subTasks] AS st ON t.[Id] = st.[UserTaskId]
						WHERE p.[IdentityGuid] = @userId
                        ORDER BY t.[Name]
                        OFFSET @offset ROWS
                        FETCH NEXT @pageSize ROWS ONLY";

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

                var userTasksDictionary = new Dictionary<int, UserTask>();
                var attachments = new List<Attachment>();
                var labels = new List<int>();
                var notes = new List<Note>();
                var subTasks = new List<SubTask>();

                var result = await connection.QueryAsync<UserTask, Attachment, Label, Note, SubTask, UserTask>(
                    GetTaskByIdSqlQuery,
                    (task, attachment, label, note, subTask) =>
                    {
                        UserTask taskEntry;

                        if (!userTasksDictionary.TryGetValue(task.tasknumber, out taskEntry))
                        {
                            taskEntry = task;
                            taskEntry.attachments = new List<Attachment>();
                            taskEntry.labels = new List<Label>();
                            taskEntry.notes = new List<Note>();
                            taskEntry.subtasks = new List<SubTask>();
                            userTasksDictionary.Add(taskEntry.tasknumber, taskEntry);
                        }

                        if (!attachments.Contains(attachment, new ValueObjectEqualityComparer()))
                        {
                            attachments.Add(attachment);
                            taskEntry.attachments.Add(attachment);
                        }

                        if (!labels.Contains(label.labelid))
                        {
                            labels.Add(label.labelid);
                            taskEntry.labels.Add(label);
                        }

                        if (!notes.Contains(note, new ValueObjectEqualityComparer()))
                        {
                            notes.Add(note);
                            taskEntry.notes.Add(note);
                        }

                        if (!subTasks.Contains(subTask, new ValueObjectEqualityComparer()))
                        {
                            subTasks.Add(subTask);
                            taskEntry.subtasks.Add(subTask);
                        }

                        return taskEntry;
                    },
                    new { id },
                    splitOn: "uri,labelid,content,subtaskname");

                var userTasks = result.ToList();
                if (userTasks.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return userTasks.First();
            }
        }

        public async Task<IEnumerable<UserTask>> GetTasksFromUserAsync(Guid userId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var userTasksDictionary = new Dictionary<int, UserTask>();
                var attachments = new List<Attachment>();
                var labels = new List<int>();
                var notes = new List<Note>();
                var subTasks = new List<SubTask>();
                var offset = pageIndex * pageSize;

                var result = await connection.QueryAsync<UserTask, Attachment, Label, Note, SubTask, UserTask>(
                    GetTasksForUserSqlQuery,
                    (task, attachment, label, note, subTask) =>
                    {
                        UserTask taskEntry;

                        if (!userTasksDictionary.TryGetValue(task.tasknumber, out taskEntry))
                        {
                            taskEntry = task;
                            taskEntry.attachments = new List<Attachment>();
                            taskEntry.labels = new List<Label>();
                            taskEntry.notes = new List<Note>();
                            taskEntry.subtasks = new List<SubTask>();
                            userTasksDictionary.Add(taskEntry.tasknumber, taskEntry);
                        }

                        if (!attachments.Contains(attachment, new ValueObjectEqualityComparer()))
                        {
                            attachments.Add(attachment);
                            taskEntry.attachments.Add(attachment);
                        }

                        if (!labels.Contains(label.labelid))
                        {
                            labels.Add(label.labelid);
                            taskEntry.labels.Add(label);
                        }

                        if (!notes.Contains(note, new ValueObjectEqualityComparer()))
                        {
                            notes.Add(note);
                            taskEntry.notes.Add(note);
                        }

                        if (!subTasks.Contains(subTask, new ValueObjectEqualityComparer()))
                        {
                            subTasks.Add(subTask);
                            taskEntry.subtasks.Add(subTask);
                        }

                        return taskEntry;
                    },
                    new { userId, offset, pageSize },
                    splitOn: "uri,labelid,content,subtaskname");

                var userTasks = result.ToList();
                if (userTasks.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return userTasks;
            }
        }
    }
}
