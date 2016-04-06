using ProjectX.Finite;

namespace ProjectX.Bnf
{
    public class Character : Production
    {
        private Finite.Character character;

        public Character(string pattern)
        {
            character = pattern;
        }

        public Character(char value)
        {
            character = value;
        }

        private Character(Finite.Character character)
        {
            this.character = character;
        }

        public override StateMachine GetStateMachine()
        {
            StateMachine stateMachine = StateMachine.BuildBasic(character);
            stateMachine.Label("character:begin", "character:end");
            return stateMachine;
        }

        public static Character operator -(Character character, char ch)
        {
            return new Character(character.character - ch);
        }

        public static Character operator -(Character character, string str)
        {
            return new Character(character.character - str);
        }

    }
}