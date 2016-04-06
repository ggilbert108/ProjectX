using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Sequence : Production
    {
        private Production[] sequence;

        public Sequence(params Production[] sequence)
        {
            this.sequence = sequence;
        }

        public override StateMachine GetStateMachine()
        {
            StateMachine result = null;
            foreach (Production production in sequence)
            {
                var productionStateMachine = production.GetStateMachine();
                if (result == null)
                {
                    result = productionStateMachine;
                }
                else
                {
                    result = StateMachine.BuildConcatenation(result, productionStateMachine);
                }
            }

            result.Label("sequence:begin", "sequence:end");
            return result;
        }
    }
}