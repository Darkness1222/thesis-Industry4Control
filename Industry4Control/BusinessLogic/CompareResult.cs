using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic
{
    internal class CompareResult
    {
        public CompareResult(bool isMatch, ControlFunction function = ControlFunction.None)
        {
            IsMatch = isMatch;
            Function = function;
        }

        public bool IsMatch
        {
            get;
            set;
        }

        public ControlFunction Function
        {
            get;
            set;
        }
    }
}
