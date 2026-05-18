using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace bangna_hospital
{
    class StockInsertReq
    {
        static int Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.Error.WriteLine("Usage: InsertMDB.exe <RequestNo> <ItemCode> <ItemName> <Qty>");
                return 1;
            }

            string requestNo = args[0];
            string itemCode = args[1];
            string itemName = args[2];
            int qty = int.Parse(args[3]);

            // Z: = Samba mount จาก Ubuntu
            string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Z:\stock.mdb;";

            try
            {
                using (var conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO StockRequest (RequestNo, ItemCode, ItemName, Qty, RequestDate) VALUES (?, ?, ?, ?, ?)";

                    cmd.Parameters.AddWithValue("RequestNo", requestNo);
                    cmd.Parameters.AddWithValue("ItemCode", itemCode);
                    cmd.Parameters.AddWithValue("ItemName", itemName);
                    cmd.Parameters.AddWithValue("Qty", qty);
                    cmd.Parameters.AddWithValue("RequestDate", DateTime.Now);

                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"OK:{rows}");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ERROR:{ex.Message}");
                return 1;
            }
        }
    }        
}
