using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;
using Quartz.Impl;

namespace LY.Quartz.HostService
{
    partial class QuartzService : ServiceBase
    {
        /// <summary>
        /// 调度程序
        /// </summary>
        private IScheduler scheduler;
        /// <summary>
        /// 异常日志记录器
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger("QuartzLogger");
        public QuartzService()
        {
            InitializeComponent();
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
        }

        /// <summary>
        /// 启动Windows服务
        /// </summary>
        public void Start()
        {
            this.OnStart(new string[0]);
        }
        protected override void OnStart(string[] args)
        {
            scheduler.Start();
            _logger.Info("Quartz服务成功启动");
            //增加一个全局捕获异常处理类
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }


        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }

        /// <summary>
        /// 全部未捕获异常捕获
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = e.ExceptionObject as Exception;
            _logger.ErrorFormat("QuartzService:{0}", error);
        }
    }
}
