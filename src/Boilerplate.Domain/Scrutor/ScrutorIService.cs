namespace Boilerplate.Domain
{
    public class ScrutorIService
    {
        #region Nested Classes

        public interface ITransientService
        {
            string GetValue();
        }

        public interface IScopedService
        {
            string GetValue();
        }

        public interface ISingletonService
        {
            string GetValue();
        }

        #endregion
    }
}