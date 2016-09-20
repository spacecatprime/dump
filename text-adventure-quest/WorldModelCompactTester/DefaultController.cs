using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextAdventures.Quest;
using TextAdventures.Quest.GameTypes;

namespace WorldModelCompactTester
{
    class DefaultController : IController
    {
        public bool m_continue = true;
        public WorldModel m_worldModel;

        internal void PrintWithColor(ConsoleColor color, string message)
        {
            var last = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = last;
        }

        internal void PrintDataWithColor<T>(ConsoleColor color, string prefix, List<T> data, Func<T, string> convert)
        {
            List<string> items = new List<string>();
            data.ForEach(d => items.Add(convert(d)));
            var last = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(prefix + " with " + string.Join(",", items.ToArray()));
            System.Console.ForegroundColor = last;
        }

        public void ShowMenu(MenuData menuData)
        {
            throw new NotImplementedException();
        }

        public void DoWait()
        {
            throw new NotImplementedException();
        }

        public void DoPause(int ms)
        {
            throw new NotImplementedException();
        }

        public void ShowQuestion(string caption)
        {
            throw new NotImplementedException();
        }

        public void SetWindowMenu(MenuData menuData)
        {
            throw new NotImplementedException();
        }

        public string GetNewGameFile(string originalFilename, string extensions)
        {
            throw new NotImplementedException();
        }

        public void PlaySound(string filename, bool synchronous, bool looped)
        {
            throw new NotImplementedException();
        }

        public void StopSound()
        {
            throw new NotImplementedException();
        }

        public void WriteHTML(string html)
        {
            throw new NotImplementedException();
        }

        public string GetURL(string file)
        {
            throw new NotImplementedException();
        }

        public void LocationUpdated(string location)
        {
            PrintWithColor(ConsoleColor.Green, "LocationUpdated:" + location);

            //var test = m_worldModel.GetObjectTypeForTypeString(ObjectType.Exit);
            //var test = m_worldModel.GetObjects("Exits");
            //if (m_worldModel.Elements.ContainsKey(ElementType.Object.ToString()))
            //{
            //    Console.WriteLine("");
            //}
        }

        public string GameName { get; set; }

        public void UpdateGameName(string name)
        {
            PrintWithColor(ConsoleColor.DarkMagenta, name);
            GameName = name;
        }

        public void ClearScreen()
        {
            throw new NotImplementedException();
        }

        public void ShowPicture(string filename)
        {
            throw new NotImplementedException();
        }

        public void SetPanesVisible(string data)
        {
            throw new NotImplementedException();
        }

        public void SetStatusText(string text)
        {
            PrintWithColor(ConsoleColor.DarkCyan, "StatusText:" + text);
        }

        public string BackgroundColor { get; set; }

        public void SetBackground(string colour)
        {
            BackgroundColor = colour;
        }

        public string ForegroundColor { get; set; }

        public void SetForeground(string colour)
        {
            ForegroundColor = colour;
        }

        public string LinkForegroundColor { get; set; }

        public void SetLinkForeground(string colour)
        {
            LinkForegroundColor = colour;
        }

        public void RunScript(string function, object[] parameters)
        {
            if ("updateExitLinks" == function)
            {
                var exitList = new List<ObjectTypeExit>();
                QuestList<string> list = (QuestList<string>)parameters[0];
                foreach (string exit in list)
                {
                    exitList.Add(new ObjectTypeExit(m_worldModel.Elements.Get(exit)));
                }
                PrintDataWithColor<ObjectTypeExit>(ConsoleColor.Red, function, exitList, delegate(ObjectTypeExit ote) { return ote.m_direction.ToString(); });
            }
            else if ("updateCommandLinks" == function)
            {
                QuestList<string> list = (QuestList<string>)parameters[0];
                var commandList = new List<ObjectTypeCommand>();
                foreach (string cmd in list)
                {
                    commandList.Add(new ObjectTypeCommand(m_worldModel.Elements.Get(cmd)));
                }
                PrintDataWithColor<ObjectTypeCommand>(ConsoleColor.Red, function, commandList, delegate(ObjectTypeCommand otc) { return otc.Element.Name; });
            }
            else if ("updateObjectLinks" == function)
            {
                var objs = ObjectTypeUtils.ConvertToObjectTypeDictionary((QuestDictionary<string>)parameters[0], m_worldModel, e => new ObjectTypeObject(e));
                PrintDataWithColor<ObjectTypeObject>(ConsoleColor.DarkYellow, function, objs.Values.ToList(), delegate(ObjectTypeObject otc) { return otc.Element.Name; });
            }
            else
            {
                PrintDataWithColor<object>(ConsoleColor.Gray, function, parameters.ToList(), delegate(object i) { return i.ToString(); });
            }
        }

        public void Quit()
        {
            System.Console.WriteLine("Quit");
            m_continue = false;
        }

        public void SetFont(string fontName)
        {
            throw new NotImplementedException();
        }

        public void SetFontSize(string fontSize)
        {
            throw new NotImplementedException();
        }

        public void Speak(string text)
        {
            PrintWithColor(ConsoleColor.Yellow, "Speak: " + text);
        }

        public void RequestSave(string html)
        {
            throw new NotImplementedException();
        }

        public void Show(string element)
        {
            System.Console.WriteLine("Show: " + element);
        }

        public void Hide(string element)
        {
            System.Console.WriteLine("Hide: " + element);
        }

        public List<string> CompassDirections { get; private set; }

        public void SetCompassDirections(IEnumerable<string> dirs)
        {
            CompassDirections = dirs.ToList();
        }

        public string InterfaceName { get; private set; }
        public string InterfaceValue { get; private set; }

        public void SetInterfaceString(string name, string text)
        {
            InterfaceName = name;
            InterfaceValue = text;
        }

        public void SetPanelContents(string html)
        {
            System.Console.WriteLine("SetPanelContents: " + html);
        }

        public void Log(string text)
        {
            System.Console.WriteLine("Log: " + text);
        }

        public string GetUIOption(UIOption option)
        {
            return "true";
        }
    }
}
