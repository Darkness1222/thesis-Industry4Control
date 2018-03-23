
namespace Industry4Control.BusinessLogic
{
    internal class CompareResult
    {

        public CompareResult(bool isMatch, int controlWordNumber = 0)
        {
            IsMatch = isMatch;
            ControlWordNumber = controlWordNumber;
        }

        public bool IsMatch
        {
            get;
            set;
        }

        public int ControlWordNumber
        {
            get;
            set;
        }
    }
}
