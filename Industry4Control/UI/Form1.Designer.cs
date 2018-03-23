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
            this.m_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // m_startServerButton
            // 
            this.m_startServerButton.Location = new System.Drawing.Point(12, 12);
            this.m_startServerButton.Name = "m_startServerButton";
            this.m_startServerButton.Size = new System.Drawing.Size(75, 23);
            this.m_startServerButton.TabIndex = 0;
            this.m_startServerButton.Text = "Start server";
            this.m_startServerButton.UseVisualStyleBackColor = true;
            // 
            // m_stopServerButton
            // 
            this.m_stopServerButton.Location = new System.Drawing.Point(12, 41);
            this.m_stopServerButton.Name = "m_stopServerButton";
            this.m_stopServerButton.Size = new System.Drawing.Size(75, 23);
            this.m_stopServerButton.TabIndex = 1;
            this.m_stopServerButton.Text = "Stop server";
            this.m_stopServerButton.UseVisualStyleBackColor = true;
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Location = new System.Drawing.Point(12, 111);
            this.m_ProgressBar.Name = "m_ProgressBar";
            this.m_ProgressBar.Size = new System.Drawing.Size(404, 23);
            this.m_ProgressBar.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 146);
            this.Controls.Add(this.m_ProgressBar);
            this.Controls.Add(this.m_stopServerButton);
            this.Controls.Add(this.m_startServerButton);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_startServerButton;
        private System.Windows.Forms.Button m_stopServerButton;
        private System.Windows.Forms.ProgressBar m_ProgressBar;
    }
}

