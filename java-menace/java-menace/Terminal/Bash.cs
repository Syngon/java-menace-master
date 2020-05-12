using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using java_menace.Physical_Objects;
using java_menace.Itens.Hardwares;

namespace java_menace.Terminal
{
    class Bash
    { 
        public RichEditBox GUI { get; set; }
        
        public Interpreter Interpreter { get; set; }
        public List<PhysicalObject> PhysicalObjects { get; set; }

        public Notebook UserNotebook { get; set; }
        public Notebook InvaderNotebook { get; set; }

        public string UserID { get; set; }
        public bool IsRunning { get; set; }
        private bool WasEverRun { get; set; }

        public Mode CurrentMode { get; set; }

        public string BufferCache;
        private int PointerStringCache;
        private List<int> StartPositionCache;
        private List<string> StringCache;
        
        public Bash(Notebook UserNotebook)
        {
            this.UserNotebook = UserNotebook;
            UserID = UserNotebook.User.Username + "@PC:~$ ";

            IsRunning = false;
            WasEverRun = false;

            PointerStringCache = 0;
            BufferCache = string.Empty;
            StartPositionCache = new List<int>();
            StringCache = new List<string>();

            SetMode(Mode.Explorer, null);

            GUI = new RichEditBox()
            {
                Margin = new Thickness(0, 515, 0, 0),
                Width = 550,
                Height = 250,
                IsSpellCheckEnabled = false,
                IsColorFontEnabled = true,
                IsEnabled = false,
                IsTextScaleFactorEnabled = false,
                Opacity = 0,
                SelectionFlyout = null,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 16.5,
                Header = "Bash"
            };

            GUI.KeyUp += KeyUp;
            GUI.TextChanged += TextChanged;
        }

        public void BootBash()
        {
            WasEverRun = true;
            Interpreter = new Interpreter(this);
            BufferCache = string.Empty;
            StartPositionCache.Add(0);
            SendMessage(UserID, true);
        }

        public void OpenBash()
        {
            if (!IsRunning)
            {
                GUI.IsEnabled = true;
                GUI.Opacity = 1;
                IsRunning = true;             
            }

            if (!WasEverRun)
            {
                BootBash();
            }
        }

        public void CloseBash()
        {
            if (IsRunning)
            {
                GUI.IsEnabled = false;
                GUI.Opacity = 0;
                IsRunning = false;
            }
        }

        public void SetMode(Mode Mode, Notebook InvaderNotebook)
        {
            this.InvaderNotebook = InvaderNotebook;
            CurrentMode = Mode;
        }

        private string GetText()
        {
            string DocumentText = string.Empty;
            GUI.Document.GetText(TextGetOptions.None, out DocumentText);
            return DocumentText;
        }

        private string FilterString(string Text)
        {
            Text = Text.Remove(Text.LastIndexOf('\r') - 1);
            var Strings = Text.Split('\r');
            Strings = Text.Split(UserID);

            return Strings[Strings.Length - 1];
        }

        private void UpdateCache()
        {
            var Text = GetText();
            BufferCache = Text.Remove(Text.LastIndexOf('\r'));
        }

        private string GetNewLine()
        {
            var Text = GetText();
            UpdateCache();
            StartPositionCache.Add(Text.LastIndexOf('\r'));
            Text = FilterString(Text);
            return Text;
        }

        private void RecordLineInfo(string Text)
        {
            if (!StringCache.Contains(Text) && !Text.Equals(""))
            {
                StringCache.Add(Text);
            }
        }

        public void SendMessage(string Text, bool CacheUpdateRequest)
        {
            var Cache = BufferCache;
            Cache += Text;
            GUI.Document.SetText(TextSetOptions.None, Cache);

            if (CacheUpdateRequest)
            {
                UpdateCache();
            }

            SetPointerAtEnd();
        }

        private void SetPointerAtEnd()
        {
            GUI.Document.Selection.SetRange(GetText().Length - 1,
                                                 GetText().Length - 1);
        }

        private void RequestStringCache(VirtualKey k)
        {
            int direction = k == VirtualKey.Up ? -1 : 1;
            PointerStringCache += direction;
            
            if(PointerStringCache > StringCache.Count - 1)
            {
                PointerStringCache = 0;
            }

            else if(PointerStringCache < 0)
            {
                PointerStringCache = StringCache.Count - 1;
            }

            SendMessage(StringCache[PointerStringCache], false);
        }

        private void CheckRequestForCache(VirtualKey k)
        {
            SetPointerAtEnd();
            string difference = GetText().Replace(BufferCache, "");
            difference = difference.Replace("\r", "");

            if (StringCache.Contains(difference) || difference.Equals(""))
            {
                RequestStringCache(k);
            }
        }

        private void RequestInterpreter(string Activator)
        {
            if(Activator != "")
            {
                Interpreter.ReceiveCommand(Activator);
            }
        }

        private void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(VirtualKey.Enter))
            {
                var NewLine = GetNewLine();
                RecordLineInfo(NewLine);
                RequestInterpreter(NewLine);
                SendMessage(UserID, true);
            }

            else if (e.Key.Equals(VirtualKey.Up) || e.Key.Equals(VirtualKey.Down))
            {
                CheckRequestForCache(e.Key);
            }
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            var DocumentText = GetText();

            if (!DocumentText.StartsWith(BufferCache))
            {
                GUI.Document.SetText(TextSetOptions.None, BufferCache);
                SetPointerAtEnd();
            }
        }
    }
}
