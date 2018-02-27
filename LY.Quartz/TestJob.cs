using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using log4net;

namespace LY.Quartz
{
    public class TestJob: IJob
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        protected static readonly ILog _log = LogManager.GetLogger(typeof(TestJob));
        /// <summary>
        /// Job执行程序
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            _log.Error($"TestJob 方法执行");
        }
    }
}
