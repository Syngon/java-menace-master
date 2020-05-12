using java_menace.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Quest
{
    class QuestBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Func<PlayableCharacter, bool> Function { get; set; }

        public QuestBase(string Title, string Description, Func<PlayableCharacter, bool> Function)
        {
            this.Description = Description;
            this.Title = Title;
            this.Function = Function;
        }
    }
}
