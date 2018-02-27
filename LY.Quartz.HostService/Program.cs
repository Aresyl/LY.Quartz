using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace LY.Quartz.HostService
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            using (QuartzService winService = new QuartzService())
            {
                winService.Start();
                Console.WriteLine("Start Service....");
                Console.ReadLine();
                winService.Stop();
            }
#else

                        if (System.Environment.CurrentDirectory == System.Environment.SystemDirectory)
                        {

                            //设置环境目录为当前程序根目录
                            System.Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                            ServiceBase[] ServicesToRun;
                            ServicesToRun = new ServiceBase[] { new QuartzService() };
                            ServiceBase.Run(ServicesToRun);

                        }
                        else
                        {
                            if (args.Length > 0 && args[0].ToLower() == "-u")
                            {
                                ProjectInstaller installer = new ProjectInstaller(ServiceName);
                                System.Configuration.Install.InstallContext context = new System.Configuration.Install.InstallContext();
                                context.Parameters.Add("assemblypath", AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.SetupInformation.ApplicationName);
                                installer.Context = context;
                                installer.Uninstall(null);
                            }
                            else
                            {
                                ServiceController sc = new ServiceController(ServiceName);
                                try
                                {
                                    if (sc.Status == ServiceControllerStatus.Stopped)
                                        sc.Start();
                                }
                                catch
                                {
                                    ProjectInstaller installer = new ProjectInstaller(ServiceName);
                                    IDictionary mySavedState = new Hashtable();
                                    System.Configuration.Install.InstallContext context = new System.Configuration.Install.InstallContext();
                                    context.Parameters.Add("assemblypath", AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.SetupInformation.ApplicationName);
                                    context.Parameters.Add("startParameters", AppDomain.CurrentDomain.BaseDirectory);
                                    installer.Context = context;
                                    installer.Install(mySavedState);
                                }
                                finally
                                {
                                    sc.Close();
                                }
                            }
                        }
#endif





            //var props = new NameValueCollection();
            ////使用简单线程池
            //props["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            ////最大线程数
            //props["quartz.threadPool.threadCount"] = "10";
            ////线程优先级：正常
            //props["quartz.threadPool.threadPriority"] = "Normal";
            ////初始化调度器
            //IScheduler scheduler = new StdSchedulerFactory(props).GetScheduler();

            ////Cron 触发器，每隔 1 秒触发一次
            //ITrigger trig = TriggerBuilder.Create().WithCronSchedule("0 59 15 * * ?").Build();
            ////将作业 Job1 加入调度计划中
            //scheduler.ScheduleJob(JobBuilder.Create<TestJob>().Build(), trig);
            ////开始执行
            //scheduler.Start();

            //Console.ReadLine();
            //scheduler.Shutdown();
        }
    }
}
