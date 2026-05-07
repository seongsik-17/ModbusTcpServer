using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacahineControl_Simulator
{
    public partial class frmMain : Form
    {
        private SimpleModbusClient _modbusClient;
        private HeatingMachine _machine;
        private bool _isSyncing = false;

        public frmMain()
        {
            InitializeComponent();
            _modbusClient = new SimpleModbusClient();
            _machine = new HeatingMachine();
            
            // UI 초기화
            UpdateUiFromMachine();
            tmrSim.Start();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_modbusClient.IsConnected)
            {
                bool success = await _modbusClient.ConnectAsync(txtIp.Text, int.Parse(txtPort.Text));
                if (success)
                {
                    btnConnect.Text = "Disconnect";
                    lblStatusValue.Text = "Connected";
                    lblStatusValue.ForeColor = Color.Green;
                    tmrModbus.Start();
                }
                else
                {
                    MessageBox.Show("Failed to connect to Modbus Server.");
                }
            }
            else
            {
                DisconnectInternal("Disconnected", Color.Gray);
            }
        }

        private void DisconnectInternal(string message, Color color)
        {
            _modbusClient.Disconnect();
            btnConnect.Text = "Connect";
            lblStatusValue.Text = message;
            lblStatusValue.ForeColor = color;
            tmrModbus.Stop();
        }

        private void tmrSim_Tick(object sender, EventArgs e)
        {
            // Update Machine Physics (dt = 0.1s)
            _machine.Update(0.1);
            
            // Update UI
            lblCurrentTemp.Text = $"{_machine.CurrentTemperature:F1} °C";
            prgHeater.Value = (int)_machine.HeaterOutput;
            
            if (_machine.IsRunning)
            {
                lblCurrentTemp.ForeColor = _machine.HeaterOutput > 0 ? Color.Red : Color.Blue;
            }
            else
            {
                lblCurrentTemp.ForeColor = Color.DarkGray;
            }
        }

        private async void tmrModbus_Tick(object sender, EventArgs e)
        {
            if (_isSyncing || !_modbusClient.IsConnected) return;

            try
            {
                _isSyncing = true;

                // 1. Read Configurations (HR 0~4)
                ushort[] configs = await _modbusClient.ReadHoldingRegistersAsync(0, 5);
                if (configs != null && configs.Length == 5)
                {
                    _machine.TargetTemperature = configs[0];
                    _machine.Pid.P = configs[1] / 100.0;
                    _machine.Pid.I = configs[2] / 100.0;
                    _machine.Pid.D = configs[3] / 100.0;
                    _machine.IsRunning = configs[4] == 1;

                    if (!numTargetTemp.Focused) numTargetTemp.Value = (decimal)_machine.TargetTemperature;
                    if (!numP.Focused) numP.Value = (decimal)_machine.Pid.P;
                    if (!numI.Focused) numI.Value = (decimal)_machine.Pid.I;
                    if (!numD.Focused) numD.Value = (decimal)_machine.Pid.D;
                }

                // 2. Write Status (HR 10~12)
                ushort[] status = new ushort[3];
                status[0] = (ushort)(_machine.CurrentTemperature * 10);
                status[1] = (ushort)_machine.HeaterOutput;
                status[2] = (ushort)(_machine.IsRunning ? 1 : 0);

                await _modbusClient.WriteMultipleRegistersAsync(10, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Modbus Sync Error: {ex.Message}");
                DisconnectInternal("Connection Lost", Color.Red);
            }
            finally
            {
                _isSyncing = false;
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            _machine.IsRunning = true;
            UpdateMachineFromUi();
            
            if (_modbusClient.IsConnected)
            {
                try {
                    await _modbusClient.WriteMultipleRegistersAsync(4, new ushort[] { 1 });
                } catch {
                    DisconnectInternal("Write Failed", Color.Red);
                }
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            _machine.IsRunning = false;
            
            if (_modbusClient.IsConnected)
            {
                try {
                    await _modbusClient.WriteMultipleRegistersAsync(4, new ushort[] { 0 });
                } catch {
                    DisconnectInternal("Write Failed", Color.Red);
                }
            }
        }

        private void UpdateMachineFromUi()
        {
            _machine.TargetTemperature = (double)numTargetTemp.Value;
            _machine.Pid.P = (double)numP.Value;
            _machine.Pid.I = (double)numI.Value;
            _machine.Pid.D = (double)numD.Value;
        }

        private void UpdateUiFromMachine()
        {
            numTargetTemp.Value = (decimal)_machine.TargetTemperature;
            numP.Value = (decimal)_machine.Pid.P;
            numI.Value = (decimal)_machine.Pid.I;
            numD.Value = (decimal)_machine.Pid.D;
        }
    }
}
