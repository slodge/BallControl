namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SpheroServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SpheroServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SpheroServiceProcessInstaller
            // 
            this.SpheroServiceProcessInstaller.Password = null;
            this.SpheroServiceProcessInstaller.Username = null;
            // 
            // SpheroServiceInstaller
            // 
            this.SpheroServiceInstaller.DisplayName = "Sphero Message Routing";
            this.SpheroServiceInstaller.ServiceName = "SpheroBluetoothService";
            this.SpheroServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SpheroServiceProcessInstaller,
            this.SpheroServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SpheroServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SpheroServiceInstaller;
    }
}