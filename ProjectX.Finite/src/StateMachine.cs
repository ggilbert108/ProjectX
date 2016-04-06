using System;
using System.Collections.Generic;

namespace ProjectX.Finite
{
    public class StateMachine
    {
        public const char EPS = '\a';
        public const char RECURSE = '\t';

        public State InitialState { get; }
        public State FinalState { get; }

        private HashSet<Character> inputs;

        public event EventHandler ReachedFinalState;

        public StateMachine(string initialLabel = "", string finalLabel = "")
        {
            InitialState = new State(initialLabel);
            FinalState = new State(finalLabel);

            FinalState.Activated += (sender, args) =>
            {
                ReachedFinalState?.Invoke(this, EventArgs.Empty);
            };

            inputs = new HashSet<Character>();
        }

        public void AddTransition(State from, State to, Character input, int channel = -1)
        {
            AddInput(input);
            if (channel >= 0)
            {
                from.AddTransition(input, to, channel);
            }
            else
            {
                from.AddTransition(input, to);
            }
        }

        public bool Validate(string input)
        {
            HashSet<Path> currentPaths = new HashSet<Path> {InitialState.GetPath()};
           
            Util.AddRangeToSet(currentPaths, GetEpsClosure(currentPaths));
            foreach (char ch in input)
            {
                if (!InputContains(ch))
                    return false;


                currentPaths = Move(currentPaths, ch);
                Util.AddRangeToSet(currentPaths, GetEpsClosure(currentPaths));
            }


            return Util.PathSetContainsState(currentPaths, FinalState);
        }

        public void Encapsulate(StateMachine inner)
        {
            AddInputFromNfa(inner);

            InitialState.AddTransition(EPS, inner.InitialState);
            inner.FinalState.AddTransition(EPS, FinalState);
        }

        #region Simulation Helpers

        public static HashSet<Path> Move(HashSet<Path> from, char input)
        {
            HashSet<Path> result = new HashSet<Path>();

            foreach (Path path in from)
            {
                HashSet<Path> fromState = path.State.OnInput(input, path.PathChannels);
                foreach (Path newPath in fromState)
                {
                    result.Add(newPath);
                }
            }

            return result;
        }

        public static HashSet<Path> GetEpsClosure(HashSet<Path> from)
        {
            HashSet<Path> closure = new HashSet<Path>();
            GetEpsClosure(from, closure);
            return closure;
        }

        private static void GetEpsClosure(HashSet<Path> from, HashSet<Path> closure)
        {
            HashSet<Path> move = Move(from, EPS);
            int added = Util.AddRangeToSet(closure, from);

            if (added == 0)
                return;

            GetEpsClosure(move, closure);
        } 

        #endregion

        #region Util

        private void AddInput(Character other)
        {
            if (other != EPS)
            {
                inputs.Add(other);
            }
        }

        public void AddInputFromNfa(StateMachine other)
        {
            foreach (Character input in other.inputs)
            {
                AddInput(input);
            }
        }

        public void Print()
        {
            InitialState.Print();
        }

        public void Label(string initialString, string finalString)
        {
            InitialState.Label = initialString;
            FinalState.Label = finalString;
        }

        private bool InputContains(char ch)
        {
            foreach (Character character in inputs)
            {
                if (character == ch)
                    return true;
            }
            return false;
        }

        #endregion

        #region Builders

        public static StateMachine BuildBasic(Character character)
        {
            StateMachine basic = new StateMachine();

            basic.AddInput(character);
            basic.AddTransition(basic.InitialState, basic.FinalState, character);

            return basic;
        }

        public static StateMachine BuildAlternation(params StateMachine[] machines)
        {
            StateMachine alt = new StateMachine();

            foreach (StateMachine machine in machines)
            {
                alt.AddInputFromNfa(machine);

                alt.AddTransition(alt.InitialState, machine.InitialState, EPS);
                alt.AddTransition(machine.FinalState, alt.FinalState, EPS);
            }

            return alt;
        }

        public static StateMachine BuildConcatenation(StateMachine a, StateMachine b)
        {
            StateMachine concat = new StateMachine();

            concat.AddInputFromNfa(a);
            concat.AddInputFromNfa(b);

            concat.AddTransition(concat.InitialState, a.InitialState, EPS);
            concat.AddTransition(a.FinalState, b.InitialState, EPS);
            concat.AddTransition(b.FinalState, concat.FinalState, EPS);

            return concat;
        }

        #endregion
    }
}