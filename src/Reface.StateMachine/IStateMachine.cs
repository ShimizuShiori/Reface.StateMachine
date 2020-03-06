using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine
{
    public interface IStateMachine<TState, TAction>
    {
        event EventHandler<StateMachinePushedEventArgs<TState, TAction>> Pushed;

        void Push(TAction action);
    }
}
