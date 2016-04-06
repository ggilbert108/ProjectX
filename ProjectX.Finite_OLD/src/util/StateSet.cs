using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Finite
{
    public class StateSet : IEnumerable<NfaState>
    {
        private readonly HashSet<NfaState> set;

        public StateSet()
        {
            set = new HashSet<NfaState>();
        }

        public void Add(NfaState nfaState)
        {
            set.Add(nfaState);
        }

        public void Remove(NfaState nfaState)
        {
            set.Remove(nfaState);
        }

        public bool Contains(NfaState nfaState)
        {
            return set.Contains(nfaState);
        }

        public void AddRange(IEnumerable<NfaState> other)
        {
            foreach (NfaState state in other)
            {
                set.Add(state);
            }
        }

        public int Count
        {
            get { return set.Count; }
        }

        public IEnumerator<NfaState> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            StateSet other = obj as StateSet;
            return other.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            int code = 0;

            foreach (NfaState state in set)
            {
                code = code ^ state.GetHashCode();
            }

            return code;
        }
    }
}