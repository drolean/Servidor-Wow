using Common.Globals;

namespace Common.Helpers
{
    public class Functions
    {
        public static bool GetCharacterSide(Races friendRace)
        {
            switch (friendRace)
            {
                case Races.Dwarf:
                case Races.Gnome:
                case Races.Human:
                case Races.NightElf:
                {
                    return false;
                }

                default:
                {
                    return true;
                }
            }
        }
    }
}