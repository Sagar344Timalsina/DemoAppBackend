using DemoAppBE.Data;
using DemoAppBE.Domain;
using DemoAppBE.Features.Dashboard.DTOs;
using DemoAppBE.Features.Dashboard.Services.Interface;
using DemoAppBE.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoAppBE.Features.Dashboard.Services.Implementation
{
    public class DashboardService(ApplicationDBContext _context, IUserContext _userContext) :IDashboardService
    {
        public async Task<Result> getAllDashboardDataAsync()
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                await using (conn)
                {
                    await conn.OpenAsync();

                    await using var cmd = conn.CreateCommand();
                    cmd.CommandText = "spDashboard"; 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@UserId", _userContext.Id));
                    cmd.Parameters.Add(new SqlParameter("@Flag", "G"));

                    await using var reader = await cmd.ExecuteReaderAsync();

                    string jsonResult = string.Empty;

                    if (await reader.ReadAsync())
                    {
                        jsonResult = reader.GetString(0); // Assuming JSON is returned as first column
                    }

                    if (string.IsNullOrWhiteSpace(jsonResult))
                        return Result.Failure(Error.Failure("400","There is no data to be shown"));

                    // Deserialize JSON into StorageResponse object
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<DashboardResponseDTO>(jsonResult, options);

                    //return result?? Result.Failure(Error.Failure("400", "There is no data to be shown"));
                  return Result.Success(result);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("400", ex.Message));
            }

        }
    }
}
