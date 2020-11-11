namespace Domain.Handlers
{
    using System;

    public class ErrorHandlerAbs
    {
        public ErrorHandlerAbs()
        {
        }
        protected internal bool TryToPerformAction<T>(Func<T> functionToRun,
                                                       Func<Exception, T> functionOnError)
        {
            try
            {
                functionToRun();
                return true;
            }
            catch (Exception ex)
            {
                functionOnError?.Invoke(ex);
                return false;
            }
        }
    }
}
