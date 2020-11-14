using FunChess.Core.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FunChess.Core.Tools
{
    public class Cloner
    {
        private readonly CoreFactory core;

        public Cloner(CoreFactory core)
        {
            this.core = core;
        }

        public T Clone<T>(object model)
            where T : class, new ()
        {
            if (!(model is T))
            {
                throw new MissmatchingCloneTypeException(typeof(T), model.GetType());
            }

            T clone = new T();

            model.GetType().GetProperties().ToList().ForEach(p => 
            {
                p.SetValue(clone, p.GetValue(model));
            });

            return clone;
        }
    }
}
