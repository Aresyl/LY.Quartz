using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace LY.Quartz.HostService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        private string ServiceName= "LY.Quartz.HostService";
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="stateSaver"></param>
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            //Start Service
            this.StartService();
        }

        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(IDictionary savedState)
        {
            this.StopService();
            base.Uninstall(savedState);
        }

        /// <summary>
        /// 启动Windows服务
        /// </summary>
        private void StartService()
        {
            try
            {
                using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController(this.ServiceName))
                {
                    if (control.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                    {
                        control.Start();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 停止Windows服务
        /// </summary>
        private void StopService()
        {
            try
            {
                using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController(this.ServiceName))
                {
                    if (control.Status != System.ServiceProcess.ServiceControllerStatus.Stopped
                        && control.Status != System.ServiceProcess.ServiceControllerStatus.StopPending)
                    {
                        control.Stop();
                    }
                    control.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
                }
            }
            catch
            {
            }
        }
    }
}
