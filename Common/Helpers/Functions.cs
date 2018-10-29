using Common.Globals;

namespace Common.Helpers
{
    public enum ManaTypes
    {
        TypeMana = 0,
        TypeRage = 1,
        TypeFocus = 2,
        TypeEnergy = 3,
        TypeHappiness = 4,
        TypeHealth = -2
    }

    public class Functions
    {
        public static ManaTypes GetClassManaType(Classes classe)
        {
            switch (classe)
            {
                case Classes.Rogue:
                    return ManaTypes.TypeEnergy;
                case Classes.Warrior:
                    return ManaTypes.TypeRage;
                default:
                    return ManaTypes.TypeMana;
            }
        }

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
