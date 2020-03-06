using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine
{
    public class StateMachinePushedEventArgs<TState, TAction> : EventArgs
    {
        public TState OldState { get; private set; }
        public TAction Action { get; private set; }
        public TState NewState { get; private set; }

        public StateMachinePushedEventArgs(TState oldState, TAction action, TState newState)
        {
            OldState = oldState;
            Action = action;
            NewState = newState;
        }
    }
}
