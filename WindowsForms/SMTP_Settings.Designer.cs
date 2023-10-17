namespace PushNotification
{
    partial class SMTP_Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SMTPServer = new Label();
            SMTPPort = new Label();
            SMTPUsername = new Label();
            SMTPPass = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            SMTPCheck1 = new CheckBox();
            SMTPEmail = new Label();
            textBox5 = new TextBox();
            SMTPcheck2 = new CheckBox();
            SMTPCancel = new Button();
            SMTPApply = new Button();
            SuspendLayout();
            // 
            // SMTPServer
            // 
            SMTPServer.AutoSize = true;
            SMTPServer.Location = new Point(40, 29);
            SMTPServer.Name = "SMTPServer";
            SMTPServer.Size = new Size(39, 15);
            SMTPServer.TabIndex = 0;
            SMTPServer.Text = "Server";
            // 
            // SMTPPort
            // 
            SMTPPort.AutoSize = true;
            SMTPPort.Location = new Point(40, 69);
            SMTPPort.Name = "SMTPPort";
            SMTPPort.Size = new Size(51, 15);
            SMTPPort.TabIndex = 1;
            SMTPPort.Text = "Port No.";
            // 
            // SMTPUsername
            // 
            SMTPUsername.AutoSize = true;
            SMTPUsername.Location = new Point(40, 108);
            SMTPUsername.Name = "SMTPUsername";
            SMTPUsername.Size = new Size(65, 15);
            SMTPUsername.TabIndex = 2;
            SMTPUsername.Text = "User Name";
            // 
            // SMTPPass
            // 
            SMTPPass.AutoSize = true;
            SMTPPass.Location = new Point(40, 149);
            SMTPPass.Name = "SMTPPass";
            SMTPPass.Size = new Size(57, 15);
            SMTPPass.TabIndex = 3;
            SMTPPass.Text = "Password";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(139, 29);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(197, 23);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(139, 69);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(197, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(139, 108);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(197, 23);
            textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(139, 149);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(197, 23);
            textBox4.TabIndex = 7;
            textBox4.UseSystemPasswordChar = true;
            // 
            // SMTPCheck1
            // 
            SMTPCheck1.AutoSize = true;
            SMTPCheck1.Location = new Point(40, 187);
            SMTPCheck1.Name = "SMTPCheck1";
            SMTPCheck1.Size = new Size(176, 19);
            SMTPCheck1.TabIndex = 8;
            SMTPCheck1.Text = "Use Common Email Address";
            SMTPCheck1.UseVisualStyleBackColor = true;
            SMTPCheck1.CheckedChanged += SMTPCheck1_CheckedChanged;
            // 
            // SMTPEmail
            // 
            SMTPEmail.AutoSize = true;
            SMTPEmail.Location = new Point(40, 220);
            SMTPEmail.Name = "SMTPEmail";
            SMTPEmail.Size = new Size(81, 15);
            SMTPEmail.TabIndex = 9;
            SMTPEmail.Text = "Email Address";
            SMTPEmail.Click += label1_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(139, 220);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(197, 23);
            textBox5.TabIndex = 10;
            // 
            // SMTPcheck2
            // 
            SMTPcheck2.AutoSize = true;
            SMTPcheck2.Location = new Point(40, 257);
            SMTPcheck2.Name = "SMTPcheck2";
            SMTPcheck2.Size = new Size(89, 19);
            SMTPcheck2.TabIndex = 11;
            SMTPcheck2.Text = "Use TLS/SSL";
            SMTPcheck2.UseVisualStyleBackColor = true;
            // 
            // SMTPCancel
            // 
            SMTPCancel.Location = new Point(341, 290);
            SMTPCancel.Name = "SMTPCancel";
            SMTPCancel.Size = new Size(64, 22);
            SMTPCancel.TabIndex = 15;
            SMTPCancel.Text = "Cancel";
            SMTPCancel.UseVisualStyleBackColor = true;
            SMTPCancel.Click += SMTPCancel_Click;
            // 
            // SMTPApply
            // 
            SMTPApply.Location = new Point(271, 290);
            SMTPApply.Name = "SMTPApply";
            SMTPApply.Size = new Size(64, 22);
            SMTPApply.TabIndex = 16;
            SMTPApply.Text = "Apply";
            SMTPApply.UseVisualStyleBackColor = true;
            SMTPApply.Click += SMTPApply_Click;
            // 
            // SMTP_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 324);
            Controls.Add(SMTPApply);
            Controls.Add(SMTPCancel);
            Controls.Add(SMTPcheck2);
            Controls.Add(textBox5);
            Controls.Add(SMTPEmail);
            Controls.Add(SMTPCheck1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(SMTPPass);
            Controls.Add(SMTPUsername);
            Controls.Add(SMTPPort);
            Controls.Add(SMTPServer);
            MaximizeBox = false;
            Name = "SMTP_Settings";
            Text = "SMTP_Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SMTPServer;
        private Label SMTPPort;
        private Label SMTPUsername;
        private Label SMTPPass;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private CheckBox SMTPCheck1;
        private Label SMTPEmail;
        private TextBox textBox5;
        private CheckBox SMTPcheck2;
        private Button SMTPCancel;
        private Button SMTPApply;
    }
}