using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.API.Application.Queries
{
    public class UserTaskQueries
        : IUserTaskQueries
    {
        private readonly string _connectionString;

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

                var result = await connection.QueryAsync<UserTask, Attachment, Label, Note, SubTask, UserTask>(
                    @"SELECT 
                            t.[Id] AS tasknumber,
                            t.[DateCreated] AS datecreated,
                            t.[Name] AS taskname,
                            t.[IsCompleted] AS iscompleted,
                            ats.[Uri] AS uri,
                            l.[Argb] AS argb,
                            l.[Name] AS labelname,
                            n.[Content] AS content,
                            st.[Name] AS subtaskname,
                            st.[IsCompleted] AS iscompleted
                        FROM [tasks].[userTasks] AS t
                        LEFT JOIN [tasks].[attachments] AS ats ON t.[Id] = ats.[UserTaskId]
                        LEFT JOIN [tasks].[labelItems] AS li ON t.[Id] = li.[UserTaskId]
                        LEFT JOIN [tasks].[labels] AS l ON li.[LabelId] = l.[Id]
                        LEFT JOIN [tasks].[notes] AS n ON t.[Id] = n.[UserTaskId]
                        LEFT JOIN [tasks].[subTasks] AS st ON t.[Id] = st.[UserTaskId]
                        WHERE t.Id=@id",
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

                        taskEntry.attachments.Add(attachment);
                        taskEntry.labels.Add(label);
                        taskEntry.notes.Add(note);
                        taskEntry.subtasks.Add(subTask);
                        return task;
                    }, 
                    new { id },
                    splitOn: "uri,argb,content,subtaskname" );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result.First();
            }
        }

        public Task<IEnumerable<UserTask>> GetTasksFromUserAsync(Guid userId)
        {
            throw new NotImplementedException();

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();

            //    var result = await connection.QueryAsync<dynamic>(
            //        @"SELECT 
            //            FROM tasks.userTasks AS t
            //            LEFT JOIN tasks.projects p ON t.ProjectId = p.Id
            //            WHERE p.IdentityGuid", new { id });
            //}
        }

        private UserTask MapUserTaskSubTasks(dynamic result)
        {
            throw new NotImplementedException();

            var userTask = new UserTask
            {

            };
        }
    }
}
