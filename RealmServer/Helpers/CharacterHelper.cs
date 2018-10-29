using Common.Globals;

namespace RealmServer.Helpers
{
    public class CharacterHelper
    {
        internal static Genders GetRaceModel(Races race, Genders gender)
        {
            switch (race)
            {
                case Races.Human:
                    return 49 + gender;
                case Races.Orc:
                    return 51 + gender;
                case Races.Dwarf:
                    return 53 + gender;
                case Races.NightElf:
                    return 55 + gender;
                case Races.Undead:
                    return 57 + gender;
                case Races.Tauren:
                    return 59 + gender;
                case Races.Gnome:
                    return 1563 + gender;
                case Races.Troll:
                    return 1478 + gender;
            }

            return 16358 + Genders.Male;
        }

        internal static float GetScale(Races race, Genders gender)
        {
            switch (race)
            {
                case Races.Tauren when gender == Genders.Male:
                    return 1.3f;
                case Races.Tauren:
                    return 1.25f;
                default:
                    return 1f;
            }
        }
    }
}
