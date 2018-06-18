using Industry4Control.Interfaces;
using Industry4Control.Constants;
using Industry4Control.Utils;
using Industry4Control.BusinessLogic.Communication;

namespace Industry4Control.BusinessLogic
{
    internal class ControlLogic
    {
        #region Const fields

        private const int m_Port = 50005;

        #endregion

        #region Private fields

        private CommunicationLogic m_CommunicationLogic;
        private IUiElement m_UIElement;
        private bool isServerRunning;

        private bool m_Function1Status;

        #endregion

        #region Public fields

        public bool Function1Status { get; private set; }
        public bool Function3Status { get; private set; }
        public bool Function2Status { get; private set; }
        
        #endregion

        #region Constructors

        public ControlLogic(IUiElement uiElement)
        {
            m_UIElement = uiElement;
            m_CommunicationLogic = new CommunicationLogic(m_UIElement, m_Port);
            m_CommunicationLogic.DataAvailable += OnDataAvailable;
            RegisterUIEvents();
        }

        private void OnDataAvailable(object sender, EventArguments.DataAvailableEventArgs e)
        {
            switch (e.AvailableDataType)
            {
                case AvailableDataType.Command:
                    bool activation = (e.ControlByte & 0x4) > 0;
                    if (!activation)
                    {
                        // Deactivation is not possible via commands
                        break;
                    }
                    switch (e.ControlByte & 0x3)
                    {
                        case 0:
                            Function1Status = true;
                            break;
                        case 1:
                            Function2Status = true;
                            break;
                        case 2:
                            Function3Status = true;
                            break;
                    }
                    m_UIElement.RefreshUI();
                    break;
                case AvailableDataType.Request:
                    if(e.ControlByte == 0x1)
                    {
                        OverallStatusMessage message = new OverallStatusMessage(Function1Status, Function2Status, Function3Status);
                        m_CommunicationLogic.Send(message, e.TcpClient);
                    }
                    break;
                case AvailableDataType.Voice:
                    if(e.ControlByte == 0x0)
                    {
                        VoiceChecker voiceChecker = new VoiceChecker();
                        CompareResult result = voiceChecker.Compare(e.Data);
                        switch (result.Function)
                        {
                            case ControlFunction.Function1:
                                Function1Status = result.IsMatch;
                                break;
                            case ControlFunction.Function2:
                                Function2Status = result.IsMatch;
                                break;
                            case ControlFunction.Function3:
                                Function3Status = result.IsMatch;
                                break;
                        }
                        m_UIElement.RefreshUI();
                        ProcessStatusMessage message = new ProcessStatusMessage(result.IsMatch, ProcessType.Control);
                        m_CommunicationLogic.Send(message, e.TcpClient);
                    }
                    else 
                    {
                        bool saved = Helper.SaveControlVoice(e.Data, (ControlFunction)e.ControlByte);
                        ProcessStatusMessage message = new ProcessStatusMessage(saved, ProcessType.Save);
                        m_CommunicationLogic.Send(message, e.TcpClient);
                    }

                    break;
            }
        }

        #endregion

        #region Private methods

        private void RegisterUIEvents()
        {
            if(m_UIElement != null)
            {
                m_UIElement.UIAction += UIActionHappened;
            }
        }

        private void UIActionHappened(object sender, EventArguments.UIActionEventArgs e)
        {
            switch (e.Action)
            {
                case UIActionType.StartServer:
                    StartServer();
                    break;
                case UIActionType.StopServer:
                    StopServer();
                    break;
            }
        }


        private void StartServer()
        {
            if (!isServerRunning)
            {
                m_CommunicationLogic.StartListening();
                isServerRunning = true;
            }
        }

        private void StopServer()
        {
            if (isServerRunning)
            {
                m_CommunicationLogic.StopListening();
                isServerRunning = false;
            }
        }

        #endregion
    }
}
