using System.Text.RegularExpressions;

namespace ProjectX.Bnf
{
    public class CharacterClass : Choice
    {
        public CharacterClass(string pattern)
        {
            for (int i = char.MinValue; i < char.MaxValue; i++)
            {
                char ch = (char) i;

                if (ch == '\a') continue;

                string str = "" + ch;

                if (Regex.IsMatch(str, pattern))
                {
                    AddChoice(new Character(ch));
                }
            }
        }

        public CharacterClass(CharacterClass other) : base(other)
        {
            
        }

        public static CharacterClass operator -(CharacterClass from, char except)
        {
            CharacterClass subtracted = new CharacterClass(from);
            subtracted.RemoveChoice(new Character(except));

            return subtracted;
        }
    }
}