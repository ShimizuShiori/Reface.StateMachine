using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.StateMachine
{
    public interface IStateMachineBuilder<TState, TAction>
    {
        IStateMachine<TState, TAction> Build();
    }
}
