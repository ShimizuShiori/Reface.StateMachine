using Reface.StateMachine.Errors;
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
            StateMoveInfo<TState, TAction> moveInfo;
            foreach (var info in infos)
            {
                if (!moveInfoDictionary.TryGetValue(info.From, out actionMap))
                {
                    actionMap = new Dictionary<TAction, StateMoveInfo<TState, TAction>>();
                    moveInfoDictionary[info.From] = actionMap;
                }

                if (!actionMap.TryGetValue(info.Action, out moveInfo))
                {
                    actionMap[info.Action] = info;
                    continue;
                }

                if (!moveInfo.To.Equals(info.To))
                    throw new CodeStateMachineBuilderBuildException($"[{info.From.ToString()}]--[{info.Action.ToString()}]-->[{info.To.ToString()} , {moveInfo.To.ToString()}] 存在两个目标，无法构建状态机");
            }
        }

        public StateMoveInfo<TState, TAction> Search(TState from, TAction when)
        {
            Dictionary<TAction, StateMoveInfo<TState, TAction>> actionMap;
            if (!this.moveInfoDictionary.TryGetValue(from, out actionMap))
            {
                DebugLogger.Error($"未能找任何可以从 {from.ToString()} 发起的状态变更");
                throw SearchMoveInfoException.CreateByNoMoveInfo();
            }
            StateMoveInfo<TState, TAction> info;
            if (!actionMap.TryGetValue(when, out info))
            {
                DebugLogger.Error($"没有定义 [{from.ToString()}]--[{when.ToString()}]--> 的状态转移");
                throw SearchMoveInfoException.CreateByNoMoveInfo();
            }
            return info;

        }
    }
}
