using System;
using System.Text.RegularExpressions;

namespace ProjectX.Finite
{
    public class Character
    {
        private readonly string value;
        private readonly bool isPattern;
        private string exceptions;

        private Character(string value, string exceptions, bool isPattern = false)
        {
            if (value[0] == '[' && isPattern)
            {
                throw new Exception("Do not put character class brackets around the pattern.");
            }

            this.value = value;
            this.isPattern = isPattern;
            this.exceptions = exceptions;
        }

        public Character(char value) :this("" + value, "", false)
        {

        }

        public Character(string pattern, string exceptions = "") : this(pattern, exceptions, true)
        {
            
        }

        public string Pattern
        {
            get
            {
                if(!isPattern)
                    throw new Exception("Can't get pattern on normal character");

                if (exceptions.Length > 0)
                    return $"[{value}-[{exceptions}]]";
                else
                    return $"[{value}]";
            }
        }

        public static implicit operator Character(char ch)
        {
            return new Character(ch);
        }

        public static implicit operator Character(string pattern)
        {
            return new Character(pattern, "");
        }

        public static bool operator ==(Character character, char ch)
        {
            if (character.isPattern)
            {
                string str = ch + "";
                return Regex.IsMatch(str, character.Pattern);
            }
            else
            {
                return character.value[0] == ch;
            }
        }

        public static bool operator !=(Character character, char ch)
        {
            return !(character == ch);
        }

        public static Character operator -(Character character, char except)
        {
            if (!character.isPattern)
            {
                throw new Exception("You can only perform character subtraction on character classes");
            }

            Character newCharacter = new Character(character.value, character.exceptions + except);
            return newCharacter;
        }

        public static Character operator -(Character character, string except)
        {
            if (!character.isPattern)
            {
                throw new Exception("You can only perform character subtraction on character classes");
            }

            Character newCharacter = new Character(character.value, character.exceptions + except);
            return newCharacter;
        }

        public override string ToString()
        {
            if (value == "\a")
            {
                return "EPS";
            }
            return (isPattern) ? Pattern : value;
        }

        #region Required Methods

        protected bool Equals(Character other)
        {
            return string.Equals(value, other.value) && isPattern == other.isPattern;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Character)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((value?.GetHashCode() ?? 0) * 397) ^ isPattern.GetHashCode();
            }
        }

        #endregion
    }
}