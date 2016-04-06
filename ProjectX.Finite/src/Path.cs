namespace ProjectX.Finite
{
    public class Path
    {
        public readonly State State;
        public readonly Transition PathChannels;

        public Path(State state, Transition prevChannels, int addedChannel)
        {
            State = state;
            PathChannels = new Transition();
            PathChannels = PathChannels | prevChannels;
            PathChannels.AddChannel(addedChannel);
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return State.GetHashCode();
        }
    }
}