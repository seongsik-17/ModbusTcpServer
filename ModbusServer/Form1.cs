namespace ModbusServer
{
	public partial class Form1 : Form
	{
		private ModbusTcpServer server;


		public Form1()
		{
			InitializeComponent();
			this.Text = "Modbus TCP Server";
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			logListBox.Items.Clear();
			serverOffBtn.Enabled = false;
		}

		public void InterfaceUpdate(string logText)
		{
			// UI 업데이트 로직을 여기에 작성하세요.
			this.Invoke(new Action(() =>
			{
				logListBox.Items.Add(logText);
			}));
		}

		public void UpdateInterfaceStatus(bool tf)
		{
			//ui 업데이트 로직 작성용
			if (tf)
			{
				this.Invoke(new Action(() =>
				{
					//실행 로직
				}));
			}
			else if (!tf)
			{
				this.Invoke(new Action(() =>
				{
					//실행로직
				}));
			}
		}

		private void serverStartBtn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(portTextbox.Text))
			{
				MessageBox.Show("포트를 입력해주세요.");
				return;
			}
			serverStartBtn.Enabled = false;
			serverOffBtn.Enabled = true;
			server = new ModbusTcpServer(int.Parse(portTextbox.Text), this.InterfaceUpdate);
			server.Start();
		}

		private void clearBtn_Click(object sender, EventArgs e)
		{
			//logTextBox.Clear();
		}

		private void serverOffBtn_Click(object sender, EventArgs e)
		{
			//종료 로직 호출
			server.Stop();
			serverStartBtn.Enabled = true;
			serverOffBtn.Enabled = false;
		}
	}
}