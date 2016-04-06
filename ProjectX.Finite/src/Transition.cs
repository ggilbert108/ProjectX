using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Finite
{
    public class Transition
    {
        public const int NULL_CHANNEL = 0;
        private HashSet<int> channels;

        public Transition(params int[] initialChannels)
        {
            channels = new HashSet<int>();
            foreach (int channel in initialChannels)
            {
                channels.Add(channel);
            }
        }

        public void AddChannel(int channel)
        {
            if (channel == 0)
                return;
            channels.Add(channel);
        }

        public bool CanPass(Transition transition)
        {
            var intersection = channels.Intersect(transition.channels);
            return intersection.Any();
        }

        public static Transition operator |(Transition a, Transition b)
        {
            var union = a.channels.Union(b.channels);
            return new Transition(union.ToArray());
        }

        public override string ToString()
        {
            string result = "";
            foreach (int n in channels)
            {
                result += n + " ";
            }
            return result;
        }
    }
}