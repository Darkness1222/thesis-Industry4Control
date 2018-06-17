
using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic
{
    internal class CompareResult
    {
        public CompareResult(bool isMatch, ControlFunction function = ControlFunction.Function1)
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
