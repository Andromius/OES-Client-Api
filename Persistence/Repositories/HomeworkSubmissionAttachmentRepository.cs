using Domain.Entities.Homeworks;
using Microsoft.AspNetCore.Http;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class HomeworkSubmissionAttachmentRepository : IHomeworkSubmissionAttachmentRepository
{
    private readonly OESAppApiDbContext _context;
    private const string TABLE_NAME = "\"HomeworkSubmissionAttachment\"";
    private const string INSERT_FILE_SQL = $"INSERT INTO {TABLE_NAME} (\"SubmissionId\", \"FileName\", \"File\") VALUES (@subId, @name, @file)";
    private const string SELECT_FILE_SQL = $"SELECT \"File\", \"FileName\" FROM {TABLE_NAME} WHERE \"Id\" = @id";
    private readonly string _connectionString;
    private NpgsqlConnection _connection;
    private NpgsqlCommand _command;
    private NpgsqlDataReader _reader;

    public HomeworkSubmissionAttachmentRepository(OESAppApiDbContext context, string connectionString)
    {
        _context = context;
        _connectionString = connectionString;
        _connection = new NpgsqlConnection(_connectionString);
    }

    public async Task SaveAttachmentAsync(IFormFile file, int submissionId)
    {
        await OpenConnection();

        using var cmd = new NpgsqlCommand(INSERT_FILE_SQL, _connection);
        cmd.Parameters.AddWithValue("@name", file.FileName);
        cmd.Parameters.AddWithValue("@subId", submissionId);
        cmd.Parameters.AddWithValue("@file", file.OpenReadStream());
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Stream?> GetAttachmentDataAsync(int id)
    {
        await OpenConnection();

        _command = new NpgsqlCommand(SELECT_FILE_SQL, _connection);
        _command.Parameters.AddWithValue("@id", id);
        _reader = await _command.ExecuteReaderAsync();
        if (!await _reader.ReadAsync())
            return null;
        return await _reader.GetStreamAsync(0);
    }

    public async ValueTask DisposeAsync()
    {
        if (_reader is not null)
            await _reader.DisposeAsync();
        if (_command is not null)
            await _command.DisposeAsync();
        if (_connection is not null)
            await _connection.DisposeAsync();
    }

    private async Task OpenConnection()
    {
        if (_connection.State is not System.Data.ConnectionState.Open)
            await _connection.OpenAsync();
    }
}
