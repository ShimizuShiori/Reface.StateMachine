using Reface.StateMachine.CodeBuilder;
using System;
using System.IO;

namespace Reface.StateMachine.CsvBuilder
{
    public class CsvStateMachineBuilder<TState, TAction> : IStateMachineBuilder<TState, TAction>
    {
        private readonly CodeStateMachineBuilder<TState, TAction> codeStateMachineBuilder = new CodeStateMachineBuilder<TState, TAction>();
        private readonly string text;
        private CsvStateMachineBuilder(string text)
        {
            this.text = text;
        }

        public static IStateMachineBuilder<TState, TAction> FromFile(string path)
        {
            string text = File.ReadAllText(path);
            return new CsvStateMachineBuilder<TState, TAction>(text);
        }

        public IStateMachine<TState, TAction> Build()
        {
            string[] rows = this.text.Split(new char[] { '\n' });
            string[] actions = rows[0].Replace("\r", "").Split(new char[] { ',' });
            for (int i = 1; i < rows.Length; i++)
            {
                string[] cells = rows[i].Replace("\r", "").Split(new char[] { ',' });
                string currentState = cells[0];
                TState fromState = (TState)Enum.Parse(typeof(TState), currentState);
                for (int j = 1; j < cells.Length; j++)
                {
                    string nextState = cells[j];
                    if (string.IsNullOrEmpty(nextState)) continue;
                    string strAction = actions[j];
                    TState toState = (TState)Enum.Parse(typeof(TState), nextState);
                    TAction action = (TAction)Enum.Parse(typeof(TAction), strAction);
                    this.codeStateMachineBuilder.Move(fromState, action, toState);
                }
            }
            return this.codeStateMachineBuilder.Build();
        }
    }
}
