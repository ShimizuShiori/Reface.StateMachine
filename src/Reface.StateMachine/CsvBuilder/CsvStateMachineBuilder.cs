using Reface.StateMachine.Attributes;
using Reface.StateMachine.CodeBuilder;
using System;
using System.IO;

namespace Reface.StateMachine.CsvBuilder
{
    public class CsvStateMachineBuilder<TState, TAction> : IStateMachineBuilder<TState, TAction>
        where TState : struct
        where TAction : struct
    {
        private readonly CodeStateMachineBuilder<TState, TAction> codeStateMachineBuilder = new CodeStateMachineBuilder<TState, TAction>();
        private readonly string text;
        private CsvStateMachineBuilder(string text)
        {
            this.text = text;
        }


        public static IStateMachineBuilder<TState, TAction> FromFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return FromStream(stream);
            }
        }

        public static IStateMachineBuilder<TState, TAction> FromStream(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            string text = System.Text.Encoding.Default.GetString(buffer);
            return new CsvStateMachineBuilder<TState, TAction>(text);
        }

        public IStateMachine<TState, TAction> Build()
        {
            string[] rows = this.text.Split(new char[] { '\n' });
            string[] actions = rows[0].Replace("\r", "").Split(new char[] { ',' });
            for (int i = 1; i < rows.Length; i++)
            {
                string[] cells = rows[i].Replace("\r", "").Split(new char[] { ',' });
                if (cells.Length == 1) continue;
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
