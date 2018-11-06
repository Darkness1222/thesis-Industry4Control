using Industry4Control.BusinessLogic;
using Industry4Control.Constants;
using Industry4Control.Data;
using Industry4Control.EventArguments;
using Industry4Control.Interfaces;
using Industry4Control.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace Industry4Control
{
    internal partial class Form1 : Form, IUiElement
    {
        private ControlLogic m_ControlLogic;

        public string PlcAddress => "192.168.1.200";

        public event EventHandler<UIActionEventArgs> UIAction;

        public Form1()
        {
            InitializeComponent();
            m_startServerButton.Click += M_startServerButton_Click;
            m_stopServerButton.Click += M_stopServerButton_Click;
            m_ControlLogic = new ControlLogic(this);

            InitializeFunctionStatus();


            // ONly for testing prarameter vector creation method

            /*short[] voice = new short[22050];
            using(StreamReader reader = new StreamReader("voice.txt"))
            {
                for(int i = 0; i < 22050; i++)
                {
                    voice[i] = short.Parse(reader.ReadLine());
                }
            }

            Helper.CreateParameterVectors(voice);*/

            SavedData loadedData = Helper.LoadSavedData();

            if(loadedData == null)
            {
                return;
            }

            foreach(ControlFunction key in loadedData.SavedFunctions.Keys)
            {
                using (StreamWriter writer = new StreamWriter("smallDtw\\pVectors" + key + ".txt"))
                {
                    foreach (ParameterVector pvector in loadedData.SavedFunctions[key].ParameterVectors)
                    {
                        short[] melSpectrum = pvector.GetMelSpectrum();
                        foreach (short value in melSpectrum)
                        {
                            writer.Write(((int)value).ToString() + " ");
                        }
                        writer.Write("\n");
                    }
                }
            }
            // end

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

        private void InitializeFunctionStatus()
        {
            lb_Function1_learned.Text = m_ControlLogic.Function1Learned ? "Yes" : "No";
            lb_Function2_learned.Text = m_ControlLogic.Function2Learned ? "Yes" : "No";
            lb_Function3_learned.Text = m_ControlLogic.Function3Learned ? "Yes" : "No";
            btn_clearFunction1.Enabled = m_ControlLogic.Function1Learned ? true : false;
            btn_clearFunction2.Enabled = m_ControlLogic.Function2Learned ? true : false;
            btn_clearFunction3.Enabled = m_ControlLogic.Function3Learned ? true : false;

            m_function1Status.Text = m_ControlLogic.Function1Status ? "Active" : "Inactive";
            m_function2Status.Text = m_ControlLogic.Function2Status ? "Active" : "Inactive";
            m_function3Status.Text = m_ControlLogic.Function3Status ? "Active" : "Inactive";
        }

        #region IUiElement

        public void SetStatusMessage(string message)
        {
            if(m_StatusMessage != null)
            {
                m_StatusMessage.Text = message;
            }
        }

        public void RefreshFunctionStatus()
        {
            if (lb_Function1_learned.InvokeRequired)
            {
                lb_Function1_learned.Invoke(new Action(() => {
                    lb_Function1_learned.Text = m_ControlLogic.Function1Learned ? "Yes" : "No";
                    btn_clearFunction1.Enabled = m_ControlLogic.Function1Learned ? true : false;
                }));
                lb_Function2_learned.Invoke(new Action(() => {
                    lb_Function2_learned.Text = m_ControlLogic.Function2Learned ? "Yes" : "No";
                    btn_clearFunction2.Enabled = m_ControlLogic.Function2Learned ? true : false;
                }));
                lb_Function3_learned.Invoke(new Action(() => {
                    lb_Function3_learned.Text = m_ControlLogic.Function3Learned ? "Yes" : "No";
                    btn_clearFunction3.Enabled = m_ControlLogic.Function3Learned ? true : false;
                }));
            }
            else
            {
                lb_Function1_learned.Text = m_ControlLogic.Function1Learned ? "Yes" : "No";
                btn_clearFunction1.Enabled = m_ControlLogic.Function1Learned ? true : false;
                lb_Function2_learned.Text = m_ControlLogic.Function2Learned ? "Yes" : "No";
                btn_clearFunction2.Enabled = m_ControlLogic.Function2Learned ? true : false;
                lb_Function3_learned.Text = m_ControlLogic.Function3Learned ? "Yes" : "No";
                btn_clearFunction3.Enabled = m_ControlLogic.Function3Learned ? true : false;
            }

            if (m_function1Status.InvokeRequired)
            {
                m_function1Status.Invoke(new Action(() => { m_function1Status.Text = m_ControlLogic.Function1Status ? "Active" : "Inactive"; }));
                m_function2Status.Invoke(new Action(() => { m_function2Status.Text = m_ControlLogic.Function2Status ? "Active" : "Inactive"; }));
                m_function3Status.Invoke(new Action(() => { m_function3Status.Text = m_ControlLogic.Function3Status ? "Active" : "Inactive"; }));
                return;
            }
            else
            {
                m_function1Status.Text = m_ControlLogic.Function1Status ? "Active" : "Inactive";
                m_function2Status.Text = m_ControlLogic.Function2Status ? "Active" : "Inactive";
                m_function3Status.Text = m_ControlLogic.Function3Status ? "Active" : "Inactive";
            }
        }

        #endregion

        private void btn_clearFunction1_Click(object sender, EventArgs e)
        {
            m_ControlLogic.Function1Learned = false;
        }

        private void btn_clearFunction2_Click(object sender, EventArgs e)
        {
            m_ControlLogic.Function2Learned = false;
        }

        private void btn_clearFunction3_Click(object sender, EventArgs e)
        {
            m_ControlLogic.Function3Learned = false;
        }
    }
}
