using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestServiceCoreOrders.Model;

namespace RestServiceCoreOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //Husk at sætte dit eget brugernavn/password ind i connectinction string
        const string conn = "Server=tcp:outyuero.database.windows.net,1433;Initial Catalog=demo;Persist Security Info=False;User ID=***;Password=***;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public List<OrderLine> Get(int id)
        {
            var result = new List<OrderLine>();

            string sql = "select ProductID, OrderQty, UnitPrice, UnitPriceDiscount FROM [salesLT].SalesOrderDetail where SalesOrderID = @orderid ";

            using (var databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();

                using (var selectCommand = new SqlCommand(sql, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@orderid", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Jeg ønsker kun at arbejde med int derfor caster jeg til int
                                int productid = reader.GetInt32(0);
                                int qty = reader.GetInt16(1);
                                int price = (int)reader.GetDecimal(2);
                                int discount = (int)reader.GetDecimal(3);

                                var orderline = new OrderLine()
                                {
                                    ProductID = productid,
                                    OrderQty = qty,
                                    UnitPrice = price,
                                    UnitPriceDiscount = discount
                                };

                                result.Add(orderline);
                            }
                        }
                    }

                }


            }

            return result;
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
