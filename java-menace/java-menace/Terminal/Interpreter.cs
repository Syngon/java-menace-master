using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static java_menace.Terminal.Command;

namespace java_menace.Terminal
{
    class Interpreter
    {
        public Dictionary<string, Command> Commands { get; set; } 
        private Bash RootBash { get; set; }

        public Interpreter(Bash RootBash)
        {
            this.RootBash = RootBash;
            Commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CommandDAO.GetCommands().ForEach(delegate (List<Command> ListMode)
            {
                ListMode.ForEach(delegate (Command Command)
                {
                    AddCommand(Command);
                });
            });
        }

        public bool IsCommandKnown(string Key)
        {
            return Commands.ContainsKey(Key);
        }

        public bool AddCommand(Command NewCommand)
        {
            if(!IsCommandKnown(NewCommand.Activator))
            {
                Commands.Add(NewCommand.Activator, NewCommand);
                return true;
            }

            return false;
        }

        public Tuple<string, string[]> TreatLine(string Line)
        {
            Line.Replace("\r", "");
            string[] Command = Line.Split(' ');
            string[] CommandArgs;

            if (Command.Length > 1)
            {
                CommandArgs = new string[Command.Length - 1];
                CommandArgs = Command.Where(x => x != Command[0]).ToArray();
            }

            else
            {
                CommandArgs = null;
            }

            return new Tuple<string, string[]>(Command[0], CommandArgs);
        }

        private object GetTarget(Mode Mode)
        {
            switch (Mode)
            {
                case Mode.Explorer:
                    return this.RootBash.PhysicalObjects;

                case Mode.Hacker:
                    return this.RootBash.InvaderNotebook.Bash;

                case Mode.Root:
                    return null;
            }

            return null;
        }

        private bool MatchModes(Command Command)
        {
            return Command.Mode == RootBash.CurrentMode || Command.Mode == Mode.Root;
        }

        public void StartResponse()
        {
            RootBash.SendMessage("\n\nResponse = {\n\n", true);
        }

        public void EndResponse()
        {
            RootBash.SendMessage("\n\n}\n\n", true);
        }

        private void RunCommand(Command Command, string[] Args)
        {
            if (Command.Activator != "clear")
            {
                StartResponse();
                Command.Function(RootBash, GetTarget(Command.Mode), Args);
                EndResponse();
            }
            else
            {
                Command.Function(RootBash, GetTarget(Command.Mode), Args);
            }
        }

        public void ReceiveCommand(string Line)
        {

            Tuple<string, string[]> CommandInfo = TreatLine(Line);

            if (IsCommandKnown(CommandInfo.Item1))
            {
                if(MatchModes(Commands[CommandInfo.Item1]))
                {
                    RunCommand(Commands[CommandInfo.Item1], CommandInfo.Item2);
                }

                else
                {
                    RootBash.SendMessage("Command \"" + CommandInfo.Item1 + "\" has an different mode than your bash\n", true);
                }
            }

            else
            {
                RootBash.SendMessage("Command \"" + CommandInfo.Item1 + "\" not found\n", true);
            }
        }
    }
}
