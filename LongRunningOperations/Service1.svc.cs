using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningOperations
{
    public class Service1 : IService1
    {
        private static Dictionary<Guid,string> _operationsStatus= new Dictionary<Guid, string>();

        public string LongRunning(int value)
        {
            var operationid = Guid.NewGuid();
            Task.Factory.StartNew(()=>DoLongTask(value,operationid));
            return  operationid.ToString();
        }

        public string CheckOperation(Guid jobjd)
        {
            if (_operationsStatus.ContainsKey(jobjd))
                return _operationsStatus[jobjd];
            return "NoJob";
        }

        private void DoLongTask(int value,Guid operationid)
        {
            
            while (true)
            {
                _operationsStatus.Add(operationid, "Running");
                for (int i = 0; i < value; i++)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
                _operationsStatus[operationid] = "Done";
                break;
            }

        }
    }
}
