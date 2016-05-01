using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Utilities
{
    public static class ErrorLogger
    {

        /// <summary>
        /// Logs Error for GreenPro Execptions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="innerexception"></param>
        /// <param name="message"></param>
        /// <param name="stackstrace"></param>
        /// <param name="logdate"></param>
        public static void LogError(string source, string innerexception, string message, string stackstrace)
        {

            GreenPro.Data.Log logs = new Data.Log();
            logs.InnerException = innerexception;
            logs.StackStrace = stackstrace;
            logs.Message = logs.Message;
            logs.LogDate = DateTime.Now;
            logs.source = source;

            GreenPro.Data.GreenProDbEntities db = new Data.GreenProDbEntities();
            db.Logs.Add(logs);
            db.SaveChanges();



        }

    }
}
