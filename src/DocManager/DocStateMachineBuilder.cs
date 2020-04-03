using Reface.StateMachine;
using Reface.StateMachine.CsvBuilder;

namespace DocManager
{
    public class DocStateMachineBuilder : IStateMachineBuilder<DocStates, DocActions>
    {
        private static readonly IStateMachineBuilder<DocStates, DocActions>
            realBuilder;

        static DocStateMachineBuilder()
        {
            var dll = typeof(DocStateMachineBuilder).Assembly;
            using (var stream = dll.GetManifestResourceStream("DocManager.Resources.Doc.csv"))
            {

                realBuilder = CsvStateMachineBuilder<DocStates, DocActions>.FromStream(stream);
            }
        }

        public IStateMachine<DocStates, DocActions> Build()
        {
            return realBuilder.Build();
        }
    }
}
