using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public interface IController
    {
        void ShowMenu(MenuData menuData);
        void DoWait();
        void DoPause(int ms);
        void ShowQuestion(string caption);
        void SetWindowMenu(MenuData menuData);
        string GetNewGameFile(string originalFilename, string extensions);
        void PlaySound(string filename, bool synchronous, bool looped);
        void StopSound();
        void WriteHTML(string html);
        string GetURL(string file);
        void LocationUpdated(string location);
        void UpdateGameName(string name);
        void ClearScreen();
        void ShowPicture(string filename);
        void SetPanesVisible(string data);
        void SetStatusText(string text);
        void SetBackground(string colour);
        void SetForeground(string colour);
        void SetLinkForeground(string colour);
        void RunScript(string function, object[] parameters);
        void Quit();
        void SetFont(string fontName);
        void SetFontSize(string fontSize);
        void Speak(string text);
        void RequestSave(string html);
        void Show(string element);
        void Hide(string element);
        void SetCompassDirections(IEnumerable<string> dirs);
        void SetInterfaceString(string name, string text);
        void SetPanelContents(string html);
        void Log(string text);
        string GetUIOption(UIOption option);
    }
}
