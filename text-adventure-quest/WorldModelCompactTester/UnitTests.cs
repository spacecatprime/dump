using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventures.Quest;

namespace WorldModelCompactTester
{
    [TestClass]
    public class UnitTests
    {
        static WorldModel s_worldModel;
        static DefaultController s_defaultController = new DefaultController();

        [ClassInitialize]
        static public void ClassInitialize(TestContext tc)
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
            s_worldModel = new WorldModel(System.IO.Path.Combine(folder, @"..\..\simple.aslx"), null);
            s_defaultController.m_worldModel = s_worldModel;
            s_worldModel.Initialise(s_defaultController);
            s_worldModel.Begin();
            //while (defaultController.m_continue)
            //{
            //    string stdin = System.Console.ReadLine();
            //    if (stdin == "quit")
            //    {
            //        defaultController.Quit();
            //    }
            //    else
            //    {
            //        s_worldModel.SendCommand(stdin);
            //    }
            //}
            //s_worldModel.Finish();
        }
        [ClassCleanup]
        static public void ClassCleanup()
        {
            s_worldModel.Finish();
        }
        [TestMethod]
        public void TestMethod()
        {
            PrintTextHandler cbPrintText = (string text) =>
            {
                System.Diagnostics.Debug.WriteLine(text);
            };

            s_defaultController.m_worldModel.PrintText += cbPrintText;
            s_worldModel.SendCommand("look");
            s_defaultController.m_worldModel.PrintText -= cbPrintText;
        }
    }
}
