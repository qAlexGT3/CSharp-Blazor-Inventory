using System.Data.SqlClient;
using Dapper;
using Inventory.Data.Models;

namespace Inventory.Data.Database
{
    public class DatabaseService
    {
        readonly IConfiguration _configuration;
        readonly string? SqlString;

        public DatabaseService(IConfiguration conf)
        {
            _configuration = conf;
            SqlString = _configuration["ConnectionString"];
        }

        public async Task<(List<Product> products, string? error)> SelectProducts()
        {
            List<Product> products = new();
            string? error = null;

            using(var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.QueryAsync<Product>("select * from products");

                    products = tmp.ToList();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return (products, error);
        }
        
        public async Task<string?> InsertProduct(Product product)
        {
            string? error = null;

            using(var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("insert into products(Nume, PartNumber, Pret, Pret_Cumparare) values (@Nume, @PartNumber, @Pret, @Pret_Cumparare)", product);
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<string?> DeleteProduct(int id)
        {
            string? error = null;

            using(var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("delete from products where id = @Id", new {Id = id});
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<string?> UpdateProduct(Product product)
        {
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("update products set Nume = @Nume, PartNumber = @PartNumber, Pret = @Pret, Pret_Cumparare = @Pret_Cumparare where Id = @Id", product);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<(List<Register> registers, string? error)> SelectRegisters()
        {
            List<Register> registers = new();
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.QueryAsync<Register>("select e.Id, e.Quantity, e.LocationId, e.ProductId, l.LocationName, p.Name as ProductName " +
                        "from enters_exits as e " +
                        "join products as p on e.ProductId = p.Id " +
                        "join locations as l on e.LocationId = l.Id " +
                        "order by e.Id asc");

                    registers = tmp.ToList();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return (registers, error);
        }

        public async Task<string?> InsertRegister(Register register)
        {
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("insert into enters_exits(Quantity, LocationId, ProductId) values (@Quantity, @LocationId, @ProductId)", register);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<string?> DeleteRegister(int id)
        {
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("delete from enters_exits where id = @Id", new { Id = id });
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<string?> UpdateRegister(Register register)
        {
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync("update enters_exits set Quantity = @Quantity, LocationId = @LocationId, ProductId = @ProductId where Id = @Id", register);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }
        
        public async Task<(List<Location> Locations, string? error)> LoadLocations()
        {
            var locations = new List<Location>();
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.QueryAsync<Location>("select * from locations");

                    locations = tmp.ToList();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return (locations, error);
        }

        public async Task<string?> InsertLocation(Location location)
        {
            string? error = null;

            using(var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.QueryAsync("insert into locations(LocationName, Disabled) values (@LocationName, @Disabled)", location);
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }

        public async Task<string?> UpdateLocation(Location location)
        {
            string? error = null;

            using (var connection = new SqlConnection(SqlString))
            {
                try
                {
                    var tmp = await connection.QueryAsync("update locations set LocationName = @LocationName, Disabled = @Disabled where Id = @Id", location);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return error;
        }
    }
}