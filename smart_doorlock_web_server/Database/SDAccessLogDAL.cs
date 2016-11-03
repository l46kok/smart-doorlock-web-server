using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server.Database
{
    public class SDAccessLogDAL
    {
        public void InsertAccess(SDAccessLogDTO data)
        {
            try
            {
                using (SDDatabase.Instance.Connection)
                {
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO access_log(AccessDateTime, Type, Data) VALUES(@acdt,@type,@data)"))
                    {
                        cmd.Parameters.AddWithValue("@acdt", data.AccessDateTime);
                        cmd.Parameters.AddWithValue("@type", data.Type);
                        cmd.Parameters.AddWithValue("@data", data.Data);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertAccess Exception: " + ex);
            }
        }

        public IEnumerable<SDAccessLogDTO> GetAccessLog()
        {
            List<SDAccessLogDTO> accessList = new List<SDAccessLogDTO>(); 
            try
            {
                using (SDDatabase.Instance.Connection)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT LogID, AccessDateTime, Type, Data FROM access_log"))
                    {
                        cmd.ExecuteNonQuery();
                        using (MySqlDataAdapter adap = new MySqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            adap.SelectCommand = cmd;
                            adap.Fill(dt);
                            foreach (DataRow dr in dt.Rows)
                            {
                                SDAccessLogDTO logInfo = new SDAccessLogDTO();
                                logInfo.LogID = (int)dr["LogID"];
                                logInfo.AccessDateTime = (DateTime)dr["AccessDateTime"];
                                logInfo.Type = (string)dr["Type"];
                                logInfo.Data = (string)dr["Data"];
                                accessList.Add(logInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAccessLog Exception: " + ex);
            }
           
            return accessList;
        }
    }
}
