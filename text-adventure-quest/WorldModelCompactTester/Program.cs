using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using TextAdventures.Quest;
using TextAdventures.Quest.GameTypes;

namespace WorldModelCompactTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/4518544/xmlreader-from-a-string-content
            WorldModel.BackupReferenceReader = (x) =>
            {
                const string coreLibFolder = @"..\..\..\WorldModelCompact\Core";

                string whereFrom = Path.Combine(coreLibFolder, @"Languages\" + x);
                if (File.Exists(whereFrom))
                {
                    Console.WriteLine(string.Format("Loading {0}", whereFrom));
                    return XmlReader.Create(new StringReader(File.ReadAllText(whereFrom)));
                }

                whereFrom = Path.Combine(coreLibFolder, x);
                if (File.Exists(whereFrom))
                {
                    Console.WriteLine(string.Format("Loading {0}", whereFrom));
                    return XmlReader.Create(new StringReader(File.ReadAllText(whereFrom)));
                }

                Console.WriteLine(string.Format("Could not find {0}", x));
                return null;
            };

            string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Substring(6).Replace("/", @"\");
            WorldModel worldModel = new WorldModel(System.IO.Path.Combine(folder, @"..\..\TheShip.aslx"), null);
            DefaultController defaultController = new DefaultController();
            defaultController.m_worldModel = worldModel;
            worldModel.Initialise(defaultController);
            worldModel.Begin();
            while (defaultController.m_continue)
            {
                string stdin = System.Console.ReadLine();
                if (stdin == "quit")
                {
                    defaultController.Quit();
                }
                else
                {
                    worldModel.SendCommand(stdin);
                }
            }
            worldModel.Finish();
        }
    }
}
