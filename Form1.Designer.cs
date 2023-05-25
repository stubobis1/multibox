namespace multiboxApp
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
            this.serverBtn = new System.Windows.Forms.Button();
            this.clientBtn = new System.Windows.Forms.Button();
            this.ipAddressBox = new System.Windows.Forms.TextBox();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.Label();
            this.sendMessageBtn = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverBtn
            // 
            this.serverBtn.Location = new System.Drawing.Point(12, 54);
            this.serverBtn.Name = "serverBtn";
            this.serverBtn.Size = new System.Drawing.Size(172, 28);
            this.serverBtn.TabIndex = 0;
            this.serverBtn.Text = "Server";
            this.serverBtn.UseVisualStyleBackColor = true;
            this.serverBtn.Click += new System.EventHandler(this.serverBtn_Click);
            // 
            // clientBtn
            // 
            this.clientBtn.Location = new System.Drawing.Point(190, 54);
            this.clientBtn.Name = "clientBtn";
            this.clientBtn.Size = new System.Drawing.Size(142, 28);
            this.clientBtn.TabIndex = 2;
            this.clientBtn.Text = "Client";
            this.clientBtn.UseVisualStyleBackColor = true;
            this.clientBtn.Click += new System.EventHandler(this.clientBtn_Click);
            // 
            // ipAddressBox
            // 
            this.ipAddressBox.Location = new System.Drawing.Point(12, 25);
            this.ipAddressBox.Name = "ipAddressBox";
            this.ipAddressBox.Size = new System.Drawing.Size(172, 23);
            this.ipAddressBox.TabIndex = 3;
            this.ipAddressBox.Text = "192.168.2.101";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(12, 90);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(320, 217);
            this.outputBox.TabIndex = 4;
            this.outputBox.Text = "";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(12, 10);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(62, 15);
            this.ipLabel.TabIndex = 5;
            this.ipLabel.Text = "IP Address";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(190, 25);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(142, 23);
            this.portBox.TabIndex = 6;
            this.portBox.Text = "9000";
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(201, 10);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(29, 15);
            this.port.TabIndex = 7;
            this.port.Text = "Port";
            // 
            // sendMessageBtn
            // 
            this.sendMessageBtn.Location = new System.Drawing.Point(207, 313);
            this.sendMessageBtn.Name = "sendMessageBtn";
            this.sendMessageBtn.Size = new System.Drawing.Size(125, 23);
            this.sendMessageBtn.TabIndex = 8;
            this.sendMessageBtn.Text = "SendMessage";
            this.sendMessageBtn.UseVisualStyleBackColor = true;
            this.sendMessageBtn.Click += new System.EventHandler(this.sendMessageBtn_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(12, 313);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(189, 23);
            this.messageBox.TabIndex = 9;
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(207, 342);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(125, 23);
            this.stop.TabIndex = 10;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 378);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.sendMessageBtn);
            this.Controls.Add(this.port);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.ipAddressBox);
            this.Controls.Add(this.clientBtn);
            this.Controls.Add(this.serverBtn);
            this.Name = "Form1";
            this.Text = "MultiBox";
            this.ResumeLayout(false);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.PerformLayout();

        }


        #endregion

        private Button serverBtn;
        private Button clientBtn;
        private TextBox ipAddressBox;
        private RichTextBox outputBox;
        private Label ipLabel;
        private TextBox portBox;
        private Label port;
        private Button sendMessageBtn;
        private TextBox messageBox;
        private Button stop;
    }
}