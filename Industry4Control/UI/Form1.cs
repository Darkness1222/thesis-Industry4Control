using Industry4Control.BusinessLogic;
using Industry4Control.Constants;
using Industry4Control.EventArguments;
using Industry4Control.Interfaces;
using System;
using System.Windows.Forms;

namespace Industry4Control
{
    public partial class Form1 : Form, IUiElement
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
        }

        private void M_startServerButton_Click(object sender, EventArgs e)
        {
            UIAction?.Invoke(this, new UIActionEventArgs(UIActionType.StartServer));
        }
    }
}
