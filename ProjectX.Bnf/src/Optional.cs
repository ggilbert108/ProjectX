using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Optional : Production
    {
        private Production option;

        public Optional(Production option)
        {
            this.option = option;
        }

        public override StateMachine GetStateMachine()
        {
            StateMachine empty = StateMachine.BuildBasic(StateMachine.EPS);
            StateMachine productionStateMachine = option.GetStateMachine();
            //TODO: add event handler

            StateMachine result = StateMachine.BuildAlternation(empty, productionStateMachine);
            result.Label("option:begin", "option:end");
            return result;
        }
    }
}