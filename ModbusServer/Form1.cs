namespace ModbusServer
{
	public partial class Form1 : Form
	{
		private ModbusTcpServer server;
		

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		public void InterfaceUpdate(string logText)
		{
			// UI 업데이트 로직을 여기에 작성하세요.
			this.Invoke(new Action(() =>
			{
				logTextBox.AppendText(logText + Environment.NewLine);
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
			server = new ModbusTcpServer(int.Parse(portTextbox.Text), this.InterfaceUpdate);
			serverStartBtn.Enabled = false;
			server.Start();
		}
	}
}