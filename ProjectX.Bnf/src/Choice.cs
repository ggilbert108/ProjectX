using System;
using System.Collections.Generic;
using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Choice : Production
    {
        private Production[] choices;
        private Production chosen;

        public Choice(params Production[] choices)
        {
            this.choices = choices;
            chosen = null;
        }

        public override StateMachine GetStateMachine()
        {
            var productionStateMachines = new List<StateMachine>();

            foreach (Production choice in choices)
            {
                var productionStateMachine = choice.GetStateMachine();

                Production permChoice = choice;
                //productionStateMachine.ReachedFinalState += (sender, args) =>
                //{
                //    chosen = permChoice;
                //    //Console.WriteLine(permChoice);
                //};

                productionStateMachines.Add(productionStateMachine);
            }


            StateMachine result = StateMachine.BuildAlternation(productionStateMachines.ToArray());
            result.Label("choice:begin", "choice:end");
            return result;
        }
    }
}