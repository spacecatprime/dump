using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using AdventureEngine.Model;

namespace AdventureEngine.Serialization
{
    public class StateReaderXML : StateReader
    {
        private XmlDocument m_dom;

        public StateReaderXML(StreamReader stream) 
            : base(stream)
        {
            m_dom = new XmlDocument();
            m_dom.Load(stream);
        }

        public bool LoadIntoGameEngine(GameEngine ge)
        {
            return true;
        }
    }
}

/**
   <?xml version="1.0" encoding="utf-8" ?>
<AdventureEngine>
  <rooms>
    
    <room id="0">
      <attibute_bag name="stats">
        <attribute key="name" value="Room One"/>
      </attibute_bag>
      <portal id="n.room_one" room_dir="n" to_portal="s.room_two"/>

    <room id="1"/>
      <attibute_bag name="stats">
        <attribute key="name" value="Room Two"/>
      </attibute_bag>
      <portal id="s.room_two" room_dir="s" to_portal="n.room_one"/>
    </room>
    
  </rooms>
</AdventureEngine>


**/
