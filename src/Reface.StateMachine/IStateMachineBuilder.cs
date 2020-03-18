using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine
{
    public interface IStateMachineBuilder<TState, TAction>
        where TState : struct
        where TAction : struct
    {
        IStateMachine<TState, TAction> Build();
    }
}
