using System;
using System.Runtime.Serialization;

namespace Reface.StateMachine.Errors
{
    public class CodeStateMachineBuilderBuildException : Exception
    {

        public CodeStateMachineBuilderBuildException()
        {
        }

        public CodeStateMachineBuilderBuildException(string message) : base(message)
        {
        }

        public CodeStateMachineBuilderBuildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CodeStateMachineBuilderBuildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
