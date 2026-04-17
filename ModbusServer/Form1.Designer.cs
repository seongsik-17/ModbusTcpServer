namespace ModbusServer
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			logTextBox = new TextBox();
			clearBtn = new Button();
			serverStartBtn = new Button();
			portTextbox = new TextBox();
			label1 = new Label();
			SuspendLayout();
			// 
			// logTextBox
			// 
			logTextBox.Location = new Point(12, 12);
			logTextBox.Multiline = true;
			logTextBox.Name = "logTextBox";
			logTextBox.ScrollBars = ScrollBars.Both;
			logTextBox.Size = new Size(447, 426);
			logTextBox.TabIndex = 0;
			// 
			// clearBtn
			// 
			clearBtn.Location = new Point(655, 394);
			clearBtn.Name = "clearBtn";
			clearBtn.Size = new Size(142, 44);
			clearBtn.TabIndex = 1;
			clearBtn.Text = "클리어";
			clearBtn.UseVisualStyleBackColor = true;
			// 
			// serverStartBtn
			// 
			serverStartBtn.Location = new Point(655, 20);
			serverStartBtn.Name = "serverStartBtn";
			serverStartBtn.Size = new Size(142, 44);
			serverStartBtn.TabIndex = 1;
			serverStartBtn.Text = "서버 시작";
			serverStartBtn.UseVisualStyleBackColor = true;
			serverStartBtn.Click += serverStartBtn_Click;
			// 
			// portTextbox
			// 
			portTextbox.Location = new Point(465, 29);
			portTextbox.Name = "portTextbox";
			portTextbox.Size = new Size(125, 27);
			portTextbox.TabIndex = 2;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(468, 6);
			label1.Name = "label1";
			label1.Size = new Size(47, 20);
			label1.TabIndex = 3;
			label1.Text = "PORT";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(9F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label1);
			Controls.Add(portTextbox);
			Controls.Add(serverStartBtn);
			Controls.Add(clearBtn);
			Controls.Add(logTextBox);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox logTextBox;
		private Button clearBtn;
		private Button serverStartBtn;
		private TextBox portTextbox;
		private Label label1;
	}
}
