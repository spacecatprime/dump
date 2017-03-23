using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureEngine.Entity
{
    // other classes that might help out
    // TODO: EntityObserver class so that events can be emitted between entities & components from a registered location
    // TODO: EntityBroadcaster; handles efficient ways to transmit events to trees of entities 

    public struct EntityId
    {
        public Guid guid;

        public EntityId(string uuid)
        {
            guid = Guid.Parse(uuid);
        }

        public EntityId(Guid aGuid)
        {
            guid = aGuid;
        }

        public EntityId(EntityId lhs)
        {
            guid = lhs.guid;
        }

        public override Boolean Equals(Object obj)
        {
            if (this.GetType().IsInstanceOfType(obj))
            {
                return this.guid == ((EntityId)obj).guid;
            }
            return false;
        }

        public override Int32 GetHashCode()
        {
            return this.guid.GetHashCode();
        }
    }
}
