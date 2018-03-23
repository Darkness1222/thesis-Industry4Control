using Industry4Control.Interfaces;
using Industry4Control.Constants;

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

        #endregion

        #region Constructors

        public ControlLogic(IUiElement uiElement)
        {
            m_UIElement = uiElement;
            m_CommunicationLogic = new CommunicationLogic(m_Port);
            m_CommunicationLogic.DataAvailable += OnDataAvailable;
            RegisterUIEvents();
        }

        private void OnDataAvailable(object sender, EventArguments.DataAvailableEventArgs e)
        {
            switch (e.AvailableDataType)
            {
                case AvailableDataType.Command:

                    break;
                case AvailableDataType.Request:

                    break;
                case AvailableDataType.Voice:
                    VoiceChecker voiceChecker = new VoiceChecker();
                    CompareResult result = voiceChecker.Compare(e.Data);
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
