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
			clearBtn = new Button();
			serverStartBtn = new Button();
			portTextbox = new TextBox();
			label1 = new Label();
			serverOffBtn = new Button();
			logListBox = new ListBox();
			SuspendLayout();
			// 
			// clearBtn
			// 
			clearBtn.Location = new Point(655, 394);
			clearBtn.Name = "clearBtn";
			clearBtn.Size = new Size(142, 44);
			clearBtn.TabIndex = 1;
			clearBtn.Text = "클리어";
			clearBtn.UseVisualStyleBackColor = true;
			clearBtn.Click += clearBtn_Click;
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
			// serverOffBtn
			// 
			serverOffBtn.Location = new Point(655, 344);
			serverOffBtn.Name = "serverOffBtn";
			serverOffBtn.Size = new Size(142, 44);
			serverOffBtn.TabIndex = 1;
			serverOffBtn.Text = "서버 종료";
			serverOffBtn.UseVisualStyleBackColor = true;
			serverOffBtn.Click += serverOffBtn_Click;
			// 
			// logListBox
			// 
			logListBox.FormattingEnabled = true;
			logListBox.Location = new Point(7, 12);
			logListBox.Name = "logListBox";
			logListBox.Size = new Size(452, 424);
			logListBox.TabIndex = 4;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(9F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(logListBox);
			Controls.Add(label1);
			Controls.Add(portTextbox);
			Controls.Add(serverStartBtn);
			Controls.Add(serverOffBtn);
			Controls.Add(clearBtn);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button clearBtn;
		private Button serverStartBtn;
		private TextBox portTextbox;
		private Label label1;
		private Button serverOffBtn;
		private ListBox logListBox;
	}
}
