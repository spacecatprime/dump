using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventures.Quest
{
    public delegate void PrintTextHandler(string text);
    public delegate void FinishedHandler();
    public delegate void ErrorHandler(string errorMessage);

    public interface ISimulation
    {
        bool Initialise(IController controller, bool? isCompiled = null);
        void Begin();
        void SendCommand(string command);
        void SendCommand(string command, IDictionary<string, string> metadata);
        void SendEvent(string eventName, string param);
        event PrintTextHandler PrintText;
        event UpdateListHandler UpdateList;
        event FinishedHandler Finished;
        event ErrorHandler LogError;
        List<string> Errors { get; }
        string Filename { get; }
        string OriginalFilename { get; }
        string SaveFilename { get; }
        void Finish();
        void Save(string filename, string html);
        byte[] Save(string html);
        string SaveExtension { get; }
        void FinishWait();
        void FinishPause();
        void SetMenuResponse(string response);
        void SetQuestionResponse(bool response);
        IEnumerable<string> GetExternalScripts();
        IEnumerable<string> GetExternalStylesheets();
        string TempFolder { get; set; }
        System.IO.Stream GetResource(string filename);
        string GetResourcePath(string filename);
        string GameID { get; }
    }
}
