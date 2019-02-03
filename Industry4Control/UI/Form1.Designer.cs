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
            this.staticLabel_status = new System.Windows.Forms.Label();
            this.staticLabel_learned = new System.Windows.Forms.Label();
            this.lb_Function1_learned = new System.Windows.Forms.Label();
            this.lb_Function2_learned = new System.Windows.Forms.Label();
            this.lb_Function3_learned = new System.Windows.Forms.Label();
            this.btn_clearFunction1 = new System.Windows.Forms.Button();
            this.btn_clearFunction2 = new System.Windows.Forms.Button();
            this.btn_clearFunction3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_plctest = new System.Windows.Forms.Button();
            this.tb_plcIpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_ServerControls.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.m_startServerButton.Click += new System.EventHandler(this.m_startServerButton_Click_1);
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
            this.lb_function1Name.Location = new System.Drawing.Point(12, 40);
            this.lb_function1Name.Name = "lb_function1Name";
            this.lb_function1Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function1Name.TabIndex = 5;
            this.lb_function1Name.Text = "Function 1:";
            // 
            // lb_function2Name
            // 
            this.lb_function2Name.AutoSize = true;
            this.lb_function2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_function2Name.Location = new System.Drawing.Point(12, 61);
            this.lb_function2Name.Name = "lb_function2Name";
            this.lb_function2Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function2Name.TabIndex = 6;
            this.lb_function2Name.Text = "Function 2:";
            // 
            // lb_function3Name
            // 
            this.lb_function3Name.AutoSize = true;
            this.lb_function3Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_function3Name.Location = new System.Drawing.Point(12, 83);
            this.lb_function3Name.Name = "lb_function3Name";
            this.lb_function3Name.Size = new System.Drawing.Size(71, 13);
            this.lb_function3Name.TabIndex = 7;
            this.lb_function3Name.Text = "Function 3:";
            // 
            // m_function1Status
            // 
            this.m_function1Status.AutoSize = true;
            this.m_function1Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_function1Status.Location = new System.Drawing.Point(89, 40);
            this.m_function1Status.Name = "m_function1Status";
            this.m_function1Status.Size = new System.Drawing.Size(45, 13);
            this.m_function1Status.TabIndex = 9;
            this.m_function1Status.Text = "Inactive";
            // 
            // m_function2Status
            // 
            this.m_function2Status.AutoSize = true;
            this.m_function2Status.Location = new System.Drawing.Point(89, 61);
            this.m_function2Status.Name = "m_function2Status";
            this.m_function2Status.Size = new System.Drawing.Size(45, 13);
            this.m_function2Status.TabIndex = 10;
            this.m_function2Status.Text = "Inactive";
            // 
            // m_function3Status
            // 
            this.m_function3Status.AutoSize = true;
            this.m_function3Status.Location = new System.Drawing.Point(89, 83);
            this.m_function3Status.Name = "m_function3Status";
            this.m_function3Status.Size = new System.Drawing.Size(45, 13);
            this.m_function3Status.TabIndex = 11;
            this.m_function3Status.Text = "Inactive";
            // 
            // staticLabel_status
            // 
            this.staticLabel_status.AutoSize = true;
            this.staticLabel_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticLabel_status.Location = new System.Drawing.Point(89, 18);
            this.staticLabel_status.Name = "staticLabel_status";
            this.staticLabel_status.Size = new System.Drawing.Size(43, 13);
            this.staticLabel_status.TabIndex = 12;
            this.staticLabel_status.Text = "Status";
            // 
            // staticLabel_learned
            // 
            this.staticLabel_learned.AutoSize = true;
            this.staticLabel_learned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticLabel_learned.Location = new System.Drawing.Point(147, 18);
            this.staticLabel_learned.Name = "staticLabel_learned";
            this.staticLabel_learned.Size = new System.Drawing.Size(53, 13);
            this.staticLabel_learned.TabIndex = 13;
            this.staticLabel_learned.Text = "Learned";
            // 
            // lb_Function1_learned
            // 
            this.lb_Function1_learned.AutoSize = true;
            this.lb_Function1_learned.Location = new System.Drawing.Point(147, 40);
            this.lb_Function1_learned.Name = "lb_Function1_learned";
            this.lb_Function1_learned.Size = new System.Drawing.Size(35, 13);
            this.lb_Function1_learned.TabIndex = 14;
            this.lb_Function1_learned.Text = "label1";
            // 
            // lb_Function2_learned
            // 
            this.lb_Function2_learned.AutoSize = true;
            this.lb_Function2_learned.Location = new System.Drawing.Point(147, 61);
            this.lb_Function2_learned.Name = "lb_Function2_learned";
            this.lb_Function2_learned.Size = new System.Drawing.Size(35, 13);
            this.lb_Function2_learned.TabIndex = 15;
            this.lb_Function2_learned.Text = "label1";
            // 
            // lb_Function3_learned
            // 
            this.lb_Function3_learned.AutoSize = true;
            this.lb_Function3_learned.Location = new System.Drawing.Point(147, 83);
            this.lb_Function3_learned.Name = "lb_Function3_learned";
            this.lb_Function3_learned.Size = new System.Drawing.Size(35, 13);
            this.lb_Function3_learned.TabIndex = 16;
            this.lb_Function3_learned.Text = "label1";
            // 
            // btn_clearFunction1
            // 
            this.btn_clearFunction1.Location = new System.Drawing.Point(203, 35);
            this.btn_clearFunction1.Name = "btn_clearFunction1";
            this.btn_clearFunction1.Size = new System.Drawing.Size(57, 23);
            this.btn_clearFunction1.TabIndex = 17;
            this.btn_clearFunction1.Text = "Clear";
            this.btn_clearFunction1.UseVisualStyleBackColor = true;
            this.btn_clearFunction1.Click += new System.EventHandler(this.btn_clearFunction1_Click);
            // 
            // btn_clearFunction2
            // 
            this.btn_clearFunction2.Location = new System.Drawing.Point(203, 56);
            this.btn_clearFunction2.Name = "btn_clearFunction2";
            this.btn_clearFunction2.Size = new System.Drawing.Size(57, 23);
            this.btn_clearFunction2.TabIndex = 18;
            this.btn_clearFunction2.Text = "Clear";
            this.btn_clearFunction2.UseVisualStyleBackColor = true;
            this.btn_clearFunction2.Click += new System.EventHandler(this.btn_clearFunction2_Click);
            // 
            // btn_clearFunction3
            // 
            this.btn_clearFunction3.Location = new System.Drawing.Point(203, 78);
            this.btn_clearFunction3.Name = "btn_clearFunction3";
            this.btn_clearFunction3.Size = new System.Drawing.Size(57, 23);
            this.btn_clearFunction3.TabIndex = 19;
            this.btn_clearFunction3.Text = "Clear";
            this.btn_clearFunction3.UseVisualStyleBackColor = true;
            this.btn_clearFunction3.Click += new System.EventHandler(this.btn_clearFunction3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_plctest);
            this.groupBox1.Controls.Add(this.tb_plcIpAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(287, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 204);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC control";
            // 
            // btn_plctest
            // 
            this.btn_plctest.Location = new System.Drawing.Point(125, 171);
            this.btn_plctest.Name = "btn_plctest";
            this.btn_plctest.Size = new System.Drawing.Size(75, 23);
            this.btn_plctest.TabIndex = 2;
            this.btn_plctest.Text = "test";
            this.btn_plctest.UseVisualStyleBackColor = true;
            // 
            // tb_plcIpAddress
            // 
            this.tb_plcIpAddress.Location = new System.Drawing.Point(72, 29);
            this.tb_plcIpAddress.Name = "tb_plcIpAddress";
            this.tb_plcIpAddress.Size = new System.Drawing.Size(128, 20);
            this.tb_plcIpAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP address:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 235);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_clearFunction3);
            this.Controls.Add(this.btn_clearFunction2);
            this.Controls.Add(this.btn_clearFunction1);
            this.Controls.Add(this.lb_Function3_learned);
            this.Controls.Add(this.lb_Function2_learned);
            this.Controls.Add(this.lb_Function1_learned);
            this.Controls.Add(this.staticLabel_learned);
            this.Controls.Add(this.staticLabel_status);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label staticLabel_status;
        private System.Windows.Forms.Label staticLabel_learned;
        private System.Windows.Forms.Label lb_Function1_learned;
        private System.Windows.Forms.Label lb_Function2_learned;
        private System.Windows.Forms.Label lb_Function3_learned;
        private System.Windows.Forms.Button btn_clearFunction1;
        private System.Windows.Forms.Button btn_clearFunction2;
        private System.Windows.Forms.Button btn_clearFunction3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_plctest;
        private System.Windows.Forms.TextBox tb_plcIpAddress;
        private System.Windows.Forms.Label label1;
    }
}

