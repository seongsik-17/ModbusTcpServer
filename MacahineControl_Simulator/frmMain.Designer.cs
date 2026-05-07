namespace MacahineControl_Simulator
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpConnection = new GroupBox();
            this.btnConnect = new Button();
            this.txtPort = new TextBox();
            this.lblPort = new Label();
            this.txtIp = new TextBox();
            this.lblIp = new Label();
            this.grpStatus = new GroupBox();
            this.lblStatusValue = new Label();
            this.lblStatus = new Label();
            this.prgHeater = new ProgressBar();
            this.lblHeaterOutput = new Label();
            this.lblCurrentTemp = new Label();
            this.grpConfig = new GroupBox();
            this.btnStop = new Button();
            this.btnRun = new Button();
            this.numD = new NumericUpDown();
            this.lblD = new Label();
            this.numI = new NumericUpDown();
            this.lblI = new Label();
            this.numP = new NumericUpDown();
            this.lblP = new Label();
            this.numTargetTemp = new NumericUpDown();
            this.lblTargetTemp = new Label();
            this.tmrSim = new System.Windows.Forms.Timer(this.components);
            this.tmrModbus = new System.Windows.Forms.Timer(this.components);
            this.grpConnection.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetTemp)).BeginInit();
            this.SuspendLayout();

            // grpConnection
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.txtPort);
            this.grpConnection.Controls.Add(this.lblPort);
            this.grpConnection.Controls.Add(this.txtIp);
            this.grpConnection.Controls.Add(this.lblIp);
            this.grpConnection.Location = new Point(12, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new Size(360, 80);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Modbus Server Connection";

            // lblIp / txtIp
            this.lblIp.Location = new Point(15, 25);
            this.lblIp.Size = new Size(30, 20);
            this.lblIp.Text = "IP:";
            this.txtIp.Location = new Point(45, 22);
            this.txtIp.Size = new Size(100, 27);
            this.txtIp.Text = "127.0.0.1";

            // lblPort / txtPort
            this.lblPort.Location = new Point(155, 25);
            this.lblPort.Size = new Size(40, 20);
            this.lblPort.Text = "Port:";
            this.txtPort.Location = new Point(200, 22);
            this.txtPort.Size = new Size(60, 27);
            this.txtPort.Text = "502";

            // btnConnect
            this.btnConnect.Location = new Point(270, 20);
            this.btnConnect.Size = new Size(80, 30);
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);

            // grpStatus
            this.grpStatus.Controls.Add(this.lblStatusValue);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Controls.Add(this.prgHeater);
            this.grpStatus.Controls.Add(this.lblHeaterOutput);
            this.grpStatus.Controls.Add(this.lblCurrentTemp);
            this.grpStatus.Location = new Point(12, 100);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new Size(360, 150);
            this.grpStatus.TabIndex = 1;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Machine Status";

            // lblCurrentTemp
            this.lblCurrentTemp.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblCurrentTemp.ForeColor = Color.DarkRed;
            this.lblCurrentTemp.Location = new Point(15, 30);
            this.lblCurrentTemp.Size = new Size(330, 50);
            this.lblCurrentTemp.Text = "25.0 °C";
            this.lblCurrentTemp.TextAlign = ContentAlignment.MiddleCenter;

            // lblHeaterOutput / prgHeater
            this.lblHeaterOutput.Location = new Point(15, 90);
            this.lblHeaterOutput.Text = "Heater Output:";
            this.prgHeater.Location = new Point(120, 90);
            this.prgHeater.Size = new Size(225, 20);

            // lblStatus
            this.lblStatus.Location = new Point(15, 120);
            this.lblStatus.Text = "Status:";
            this.lblStatusValue.Location = new Point(120, 120);
            this.lblStatusValue.Text = "Disconnected";
            this.lblStatusValue.ForeColor = Color.Gray;

            // grpConfig
            this.grpConfig.Controls.Add(this.btnStop);
            this.grpConfig.Controls.Add(this.btnRun);
            this.grpConfig.Controls.Add(this.numD);
            this.grpConfig.Controls.Add(this.lblD);
            this.grpConfig.Controls.Add(this.numI);
            this.grpConfig.Controls.Add(this.lblI);
            this.grpConfig.Controls.Add(this.numP);
            this.grpConfig.Controls.Add(this.lblP);
            this.grpConfig.Controls.Add(this.numTargetTemp);
            this.grpConfig.Controls.Add(this.lblTargetTemp);
            this.grpConfig.Location = new Point(12, 260);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new Size(360, 180);
            this.grpConfig.TabIndex = 2;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "Control & Configuration";

            // Target Temp
            this.lblTargetTemp.Location = new Point(15, 30);
            this.lblTargetTemp.Text = "Target Temp:";
            this.numTargetTemp.Location = new Point(120, 28);
            this.numTargetTemp.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numTargetTemp.Value = 25;

            // PID P
            this.lblP.Location = new Point(15, 65);
            this.lblP.Text = "PID - P:";
            this.numP.Location = new Point(65, 63);
            this.numP.DecimalPlaces = 2;
            this.numP.Value = 1.0m;
            this.numP.Increment = 0.1m;

            // PID I
            this.lblI.Location = new Point(135, 65);
            this.lblI.Text = "I:";
            this.numI.Location = new Point(160, 63);
            this.numI.DecimalPlaces = 3;
            this.numI.Value = 0.05m;
            this.numI.Increment = 0.01m;

            // PID D
            this.lblD.Location = new Point(235, 65);
            this.lblD.Text = "D:";
            this.numD.Location = new Point(265, 63);
            this.numD.DecimalPlaces = 3;
            this.numD.Value = 0.01m;
            this.numD.Increment = 0.01m;

            // Buttons
            this.btnRun.BackColor = Color.LightGreen;
            this.btnRun.Location = new Point(15, 120);
            this.btnRun.Size = new Size(160, 40);
            this.btnRun.Text = "RUN MACHINE";
            this.btnRun.Click += new EventHandler(this.btnRun_Click);

            this.btnStop.BackColor = Color.LightCoral;
            this.btnStop.Location = new Point(185, 120);
            this.btnStop.Size = new Size(160, 40);
            this.btnStop.Text = "STOP MACHINE";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);

            // tmrSim
            this.tmrSim.Interval = 100;
            this.tmrSim.Tick += new EventHandler(this.tmrSim_Tick);

            // tmrModbus
            this.tmrModbus.Interval = 500;
            this.tmrModbus.Tick += new EventHandler(this.tmrModbus_Tick);

            // frmMain
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(384, 461);
            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpConnection);
            this.Name = "frmMain";
            this.Text = "HeatControl Simulator (PID)";
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpConfig.ResumeLayout(false);
            this.grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetTemp)).EndInit();
            this.ResumeLayout(false);
        }

        private GroupBox grpConnection;
        private Label lblIp;
        private TextBox txtIp;
        private Label lblPort;
        private TextBox txtPort;
        private Button btnConnect;
        private GroupBox grpStatus;
        private Label lblCurrentTemp;
        private Label lblHeaterOutput;
        private ProgressBar prgHeater;
        private Label lblStatus;
        private Label lblStatusValue;
        private GroupBox grpConfig;
        private Label lblTargetTemp;
        private NumericUpDown numTargetTemp;
        private Label lblP;
        private NumericUpDown numP;
        private Label lblI;
        private NumericUpDown numI;
        private Label lblD;
        private NumericUpDown numD;
        private Button btnRun;
        private Button btnStop;
        private System.Windows.Forms.Timer tmrSim;
        private System.Windows.Forms.Timer tmrModbus;
    }
}
