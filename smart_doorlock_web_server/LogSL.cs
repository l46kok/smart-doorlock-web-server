using smart_doorlock_web_server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server
{
    public class LogSL
    {
        private static readonly LogSL _instance = new LogSL();
        public static LogSL Instance
        {
            get
            {
                return _instance;
            }
        }

        private LogSL() { }
        public List<LogData> GetAccessLog()
        {
            List<LogData> dataList = new List<LogData>();
            List<SDAccessLogDTO> accessLogDTO = SDAccessLogDAL.GetAccessLog().ToList();
            foreach (SDAccessLogDTO accessLog in accessLogDTO)
            {
                LogData logData = new LogData();
                logData.AccessDateTime = accessLog.AccessDateTime;
                logData.Type = accessLog.Type;
                logData.AccessID = accessLog.Data;
                dataList.Add(logData);
            }

            return dataList;
        }
    }
}
