using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine.CodeBuilder
{
    public class StateMoveInfo<TState, TAction>
    {
        public TState From { get; private set; }
        public TAction Action { get; private set; }
        public TState To { get; private set; }

        public StateMoveInfo(TState from, TAction action, TState to)
        {
            From = from;
            Action = action;
            To = to;
        }

        public override string ToString()
        {
            return $"[{this.From.ToString()}]--[{this.Action.ToString()}]-->[{this.To.ToString()}]";
        }
    }
}
