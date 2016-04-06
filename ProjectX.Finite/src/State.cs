using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ProjectX.Finite
{
    public class State
    {
        public const int RESERVED_CHANNEL = 1;
        private static int channelCount = RESERVED_CHANNEL + 1;
        private static int currentId = 0;

        private List<Character> inputs;
        private List<State> states;
        private List<Transition> transitions;

        private HashSet<string> transitionRecords; 

        private int id;
        private int addChannel;

        public string Label { get; set; }

        public event EventHandler Activated;

        public State(string label)
        {
            inputs = new List<Character>();
            states = new List<State>();
            transitions = new List<Transition>();

            transitionRecords = new HashSet<string>();

            addChannel = RESERVED_CHANNEL;

            this.Label = label;
            id = currentId++;
        }

        public int SetAddChannel()
        {
            addChannel = channelCount++;
            return addChannel;
        }

        public void AddTransition(Character input, State to, int requiredChannel = RESERVED_CHANNEL)
        {
            string record = GetRecord(input, to, requiredChannel);
            if (transitionRecords.Contains(record))
            {
                return;
            }
            transitionRecords.Add(record);

            inputs.Add(input);
            states.Add(to);
            transitions.Add(new Transition(requiredChannel));
        }

        public HashSet<Path> OnInput(char input, Transition throughChannels)
        {
            HashSet<Path> result = new HashSet<Path>();
            for(int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] != input) continue;

                //if (!transitions[i].CanPass(throughChannels))
                //{
                //    //Console.WriteLine("cant pass" + transitions[i] + " " + throughChannels);
                //    continue;
                //}

                State to = states[i];
                to.Activate();

                Transition pathTransition = new Transition();
                Path path = new Path(to, throughChannels, addChannel);

                result.Add(path);
            }

            return result;
        }

        public bool IsConnectedTo(State other)
        {
            if (other.Equals(this))
            {
                return true;
            }
            bool result = false;
            foreach (State state in states)
            {
                result = result || state.IsConnectedTo(other);
            }
            return result;
        }

        public Path GetPath()
        {
            return new Path(this, new Transition(RESERVED_CHANNEL), addChannel);
        }

        public void Activate()
        {
            Activated?.Invoke(this, EventArgs.Empty);
        }

        public void Print()
        {
            HashSet<int> printed = new HashSet<int>();
            Print(printed);
        }

        public void Print(HashSet<int> printed)
        {
            if (printed.Contains(id))
            {
                return;
            }

            printed.Add(id);

            Console.WriteLine(Label == "" ? "" + id : Label);
            for (int i = 0; i < inputs.Count; i++)
            {
                string character = inputs[i] == StateMachine.EPS ? "EPS" : inputs[i] + "";
                string label = states[i].Label == "" ? "" + states[i].id : states[i].Label;
                Console.WriteLine($"\t{character} -> {label} : {transitions[i]}");
            }

            foreach (State state in states)
            {
                state.Print(printed);
            }
        }

        private string GetRecord(Character character, State state, int requiredChannel)
        {
            return character.ToString() + state.GetHashCode() + requiredChannel;
        }
    }
}