using System;
using System.Collections.Generic;

namespace Reface.StateMachine.CodeBuilder
{
    public class DefaultStateMoveInfoSearcher<TState, TAction> : IStateMoveInfoSearcher<TState, TAction>
    {
        private Dictionary<TState, Dictionary<TAction, StateMoveInfo<TState, TAction>>> moveInfoDictionary
            = new Dictionary<TState, Dictionary<TAction, StateMoveInfo<TState, TAction>>>();

        public DefaultStateMoveInfoSearcher(IEnumerable<StateMoveInfo<TState, TAction>> infos)
        {
            Dictionary<TAction, StateMoveInfo<TState, TAction>> actionMap;
            foreach (var info in infos)
            {
                if (!moveInfoDictionary.TryGetValue(info.From, out actionMap))
                {
                    actionMap = new Dictionary<TAction, StateMoveInfo<TState, TAction>>();
                    moveInfoDictionary[info.From] = actionMap;
                }
                actionMap[info.Action] = info;
            }
        }

        public StateMoveInfo<TState, TAction> Search(TState from, TAction when)
        {
            Dictionary<TAction, StateMoveInfo<TState, TAction>> actionMap;
            if (!this.moveInfoDictionary.TryGetValue(from, out actionMap))
            {
                throw new ApplicationException($"no moves from [{from.ToString()}]");
            }
            StateMoveInfo<TState, TAction> info;
            if (!actionMap.TryGetValue(when, out info))
            {
                throw new ApplicationException($"no moves [{from.ToString()}]--[{when.ToString()}]-->");
            }
            return info;

        }
    }
}
