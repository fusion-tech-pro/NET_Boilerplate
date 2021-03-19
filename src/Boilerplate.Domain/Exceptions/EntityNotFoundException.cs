namespace Boilerplate.Domain
{
    #region << Using >>

    using System;

    #endregion

    public class EntityNotFoundException : Exception
    {
        #region Constructors

        public EntityNotFoundException(string name, object key)
                : base($"Entity '{name}' ({key}) was not found.") { }

        #endregion
    }
}