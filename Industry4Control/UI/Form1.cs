using Industry4Control.BusinessLogic;
using Industry4Control.Constants;
using Industry4Control.EventArguments;
using Industry4Control.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Industry4Control
{
    internal partial class Form1 : Form, IUiElement
    {
        private ControlLogic m_ControlLogic;
        public event EventHandler<UIActionEventArgs> UIAction;

        public Form1()
        {
            InitializeComponent();
            m_startServerButton.Click += M_startServerButton_Click;
            m_stopServerButton.Click += M_stopServerButton_Click;
            m_ControlLogic = new ControlLogic(this);
        }

        private void M_stopServerButton_Click(object sender, EventArgs e)
        {
            UIAction?.Invoke(this, new UIActionEventArgs(UIActionType.StopServer));
            m_stopServerButton.Enabled = false;
            m_startServerButton.Enabled = true;
        }

        private void M_startServerButton_Click(object sender, EventArgs e)
        {
            UIAction?.Invoke(this, new UIActionEventArgs(UIActionType.StartServer));
            m_stopServerButton.Enabled = true;
            m_startServerButton.Enabled = false;
        }

        #region IUiElement

        public void SetStatusMessage(string message)
        {
            if(m_StatusMessage != null)
            {
                m_StatusMessage.Text = message;
            }
        }

        public void RefreshUI()
        {
            m_function1Status.Text = m_ControlLogic.Function1Status ? "Active" : "Inactive";
            m_function2Status.Text = m_ControlLogic.Function2Status ? "Active" : "Inactive";
            m_function3Status.Text = m_ControlLogic.Function3Status ? "Active" : "Inactive";
        }

        #endregion
    }
}
