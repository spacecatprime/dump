using AdventureEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Component
{
    public abstract class AbstractComponent<T> : IComponent
    {
        public String TypeId
        {
            get { return typeof(T).ToString(); }
        }

        public abstract Boolean OnEnd(Object context);
        public abstract Boolean OnInit(Object context);
        public abstract Boolean OnStart(Object context);
    }
}
