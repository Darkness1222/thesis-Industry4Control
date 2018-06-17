namespace Industry4Control
{
    partial class Form1
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
            if(m_startServerButton != null)
            {
                m_startServerButton.Click -= M_startServerButton_Click;
            }

            if(m_stopServerButton != null)
            {
                m_stopServerButton.Click -= M_stopServerButton_Click;
            }

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
            this.m_startServerButton = new System.Windows.Forms.Button();
            this.m_stopServerButton = new System.Windows.Forms.Button();
            this.lb_Status = new System.Windows.Forms.Label();
            this.gb_ServerControls = new System.Windows.Forms.GroupBox();
            this.m_StatusMessage = new System.Windows.Forms.Label();
            this.lb_function1Name = new System.Windows.Forms.Label();
            this.lb_function2Name = new System.Windows.Forms.Label();
            this.lb_function3Name = new System.Windows.Forms.Label();
            this.m_function1Status = new System.Windows.Forms.Label();
            this.m_function2Status = new System.Windows.Forms.Label();
            this.m_function3Status = new System.Windows.Forms.Label();
            this.gb_ServerControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_startServerButton
            // 
            this.m_startServerButton.Location = new System.Drawing.Point(18, 19);
            this.m_startServerButton.Name = "m_startServerButton";
            this.m_startServerButton.Size = new System.Drawing.Size(75, 23);
            this.m_startServerButton.TabIndex = 0;
            this.m_startServerButton.Text = "Start server";
            this.m_startServerButton.UseVisualStyleBackColor = true;
            // 
            // m_stopServerButton
            // 
            this.m_stopServerButton.Enabled = false;
            this.m_stopServerButton.Location = new System.Drawing.Point(18, 48);
            this.m_stopServerButton.Name = "m_stopServerButton";
            this.m_stopServerButton.Size = new System.Drawing.Size(75, 23);
            this.m_stopServerButton.TabIndex = 1;
            this.m_stopServerButton.Text = "Stop server";
            this.m_stopServerButton.UseVisualStyleBackColor = true;
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Status.Location = new System.Drawing.Point(135, 58);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(47, 13);
            this.lb_Status.TabIndex = 2;
            this.lb_Status.Text = "Status:";
            this.lb_Status.Visible = false;
            // 
            // gb_ServerControls
            // 
            this.gb_ServerControls.Controls.Add(this.m_startServerButton);
            this.gb_ServerControls.Controls.Add(this.m_stopServerButton);
            this.gb_ServerControls.Controls.Add(this.m_StatusMessage);
            this.gb_ServerControls.Controls.Add(this.lb_Status);
            this.gb_ServerControls.Location = new System.Drawing.Point(12, 147);
            this.gb_ServerControls.Name = "gb_ServerControls";
            this.gb_ServerControls.Size = new System.Drawing.Size(259, 82);
            this.gb_ServerControls.TabIndex = 3;
            this.gb_ServerControls.TabStop = false;
            this.gb_ServerControls.Text = "Server controls";
            // 
            // m_StatusMessage
            // 
            this.m_StatusMessage.AutoSize = true;
            this.m_StatusMessage.Location = new System.Drawing.Point(188, 58);
            this.m_StatusMessage.Name = "m_StatusMessage";
            this.m_StatusMessage.Size = new System.Drawing.Size(22, 13);
            this.m_StatusMessage.TabIndex = 8;
            this.m_StatusMessage.Text = "OK";
            // 
            // lb_function1Name
            // 
            this.lb_function1Name.AutoSize = true;
            this.lb_function1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_function1Name.Location = new System.Drawing.Point(13, 13);
            this.lb_function1Name.Name = "lb_function1Name";
            this.lb_function1Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function1Name.TabIndex = 5;
            this.lb_function1Name.Text = "Function 1:";
            // 
            // lb_function2Name
            // 
            this.lb_function2Name.AutoSize = true;
            this.lb_function2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_function2Name.Location = new System.Drawing.Point(13, 34);
            this.lb_function2Name.Name = "lb_function2Name";
            this.lb_function2Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function2Name.TabIndex = 6;
            this.lb_function2Name.Text = "Function 2:";
            // 
            // lb_function3Name
            // 
            this.lb_function3Name.AutoSize = true;
            this.lb_function3Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_function3Name.Location = new System.Drawing.Point(13, 56);
            this.lb_function3Name.Name = "lb_function3Name";
            this.lb_function3Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function3Name.TabIndex = 7;
            this.lb_function3Name.Text = "Function 3:";
            // 
            // m_function1Status
            // 
            this.m_function1Status.AutoSize = true;
            this.m_function1Status.Location = new System.Drawing.Point(90, 13);
            this.m_function1Status.Name = "m_function1Status";
            this.m_function1Status.Size = new System.Drawing.Size(45, 13);
            this.m_function1Status.TabIndex = 9;
            this.m_function1Status.Text = "Inactive";
            // 
            // m_function2Status
            // 
            this.m_function2Status.AutoSize = true;
            this.m_function2Status.Location = new System.Drawing.Point(90, 34);
            this.m_function2Status.Name = "m_function2Status";
            this.m_function2Status.Size = new System.Drawing.Size(45, 13);
            this.m_function2Status.TabIndex = 10;
            this.m_function2Status.Text = "Inactive";
            // 
            // m_function3Status
            // 
            this.m_function3Status.AutoSize = true;
            this.m_function3Status.Location = new System.Drawing.Point(90, 56);
            this.m_function3Status.Name = "m_function3Status";
            this.m_function3Status.Size = new System.Drawing.Size(45, 13);
            this.m_function3Status.TabIndex = 11;
            this.m_function3Status.Text = "Inactive";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 235);
            this.Controls.Add(this.m_function3Status);
            this.Controls.Add(this.m_function2Status);
            this.Controls.Add(this.m_function1Status);
            this.Controls.Add(this.lb_function3Name);
            this.Controls.Add(this.lb_function2Name);
            this.Controls.Add(this.lb_function1Name);
            this.Controls.Add(this.gb_ServerControls);
            this.Name = "Form1";
            this.Text = "Server";
            this.gb_ServerControls.ResumeLayout(false);
            this.gb_ServerControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_startServerButton;
        private System.Windows.Forms.Button m_stopServerButton;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.GroupBox gb_ServerControls;
        private System.Windows.Forms.Label lb_function1Name;
        private System.Windows.Forms.Label lb_function2Name;
        private System.Windows.Forms.Label lb_function3Name;
        private System.Windows.Forms.Label m_StatusMessage;
        private System.Windows.Forms.Label m_function1Status;
        private System.Windows.Forms.Label m_function2Status;
        private System.Windows.Forms.Label m_function3Status;
    }
}

