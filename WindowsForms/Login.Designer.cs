namespace PushNotification
{
    partial class Login
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
            components = new System.ComponentModel.Container();
            ServerLabel = new Label();
            UsernameLabel = new Label();
            PasswordLabel = new Label();
            DBNameLabel = new Label();
            ServerText = new TextBox();
            UsernameText = new TextBox();
            PassText = new TextBox();
            DBText = new TextBox();
            OKButton = new Button();
            CancelButton = new Button();
            dBConfigConnectionBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dBConfigConnectionBindingSource).BeginInit();
            SuspendLayout();
            // 
            // ServerLabel
            // 
            ServerLabel.AutoSize = true;
            ServerLabel.Location = new Point(43, 21);
            ServerLabel.Name = "ServerLabel";
            ServerLabel.Size = new Size(39, 15);
            ServerLabel.TabIndex = 0;
            ServerLabel.Text = "Server";
            ServerLabel.Click += label1_Click;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Location = new Point(43, 51);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(60, 15);
            UsernameLabel.TabIndex = 1;
            UsernameLabel.Text = "Username";
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(43, 81);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(57, 15);
            PasswordLabel.TabIndex = 2;
            PasswordLabel.Text = "Password";
            // 
            // DBNameLabel
            // 
            DBNameLabel.AutoSize = true;
            DBNameLabel.Location = new Point(43, 112);
            DBNameLabel.Name = "DBNameLabel";
            DBNameLabel.Size = new Size(60, 15);
            DBNameLabel.TabIndex = 3;
            DBNameLabel.Text = "DB Name ";
            // 
            // ServerText
            // 
            ServerText.Location = new Point(117, 21);
            ServerText.Name = "ServerText";
            ServerText.Size = new Size(164, 23);
            ServerText.TabIndex = 4;
            ServerText.TextChanged += ServerText_TextChanged;
            // 
            // UsernameText
            // 
            UsernameText.Location = new Point(117, 51);
            UsernameText.Name = "UsernameText";
            UsernameText.Size = new Size(164, 23);
            UsernameText.TabIndex = 5;
            // 
            // PassText
            // 
            PassText.ForeColor = SystemColors.WindowText;
            PassText.HideSelection = false;
            PassText.Location = new Point(117, 81);
            PassText.Name = "PassText";
            PassText.Size = new Size(164, 23);
            PassText.TabIndex = 6;
            PassText.UseSystemPasswordChar = true;
            // 
            // DBText
            // 
            DBText.Location = new Point(117, 112);
            DBText.Name = "DBText";
            DBText.Size = new Size(164, 23);
            DBText.TabIndex = 7;
            // 
            // OKButton
            // 
            OKButton.ForeColor = SystemColors.ControlText;
            OKButton.Location = new Point(98, 161);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 8;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(191, 161);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 9;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // dBConfigConnectionBindingSource
            // 
            dBConfigConnectionBindingSource.DataSource = typeof(Model.DBConfigConnectionDTO);
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CausesValidation = false;
            ClientSize = new Size(371, 216);
            Controls.Add(CancelButton);
            Controls.Add(OKButton);
            Controls.Add(DBText);
            Controls.Add(PassText);
            Controls.Add(UsernameText);
            Controls.Add(ServerText);
            Controls.Add(DBNameLabel);
            Controls.Add(PasswordLabel);
            Controls.Add(UsernameLabel);
            Controls.Add(ServerLabel);
            MaximizeBox = false;
            Name = "Login";
            Text = "Login";
            TransparencyKey = Color.FromArgb(128, 255, 255);
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dBConfigConnectionBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ServerLabel;
        private Label UsernameLabel;
        private Label PasswordLabel;
        private Label DBNameLabel;
        private TextBox ServerText;
        private TextBox UsernameText;
        private TextBox PassText;
        private TextBox DBText;
        private Button OKButton;
        private Button CancelButton;
        private BindingSource dBConfigConnectionBindingSource;
    }
}