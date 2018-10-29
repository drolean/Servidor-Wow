using System;
using System.Threading;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using RealmServer.Enums;

namespace RealmServer.World.Managers
{
    public class TimerManager
    {
        public const int CycleTimer = 2; // Timer period (sec)

        private static bool _regenerationWorking;
        public static Timer RegenerationTimer { get; set; }

        internal static void Boot()
        {
            RegenerationTimer = new Timer(Regenerate, null, 10000, CycleTimer * 1000);

            Log.Print(LogType.RealmServer, "Loading TimerManager ".PadRight(40, '.') + " [OK] ");
        }

        private static void Regenerate(object state)
        {
#if DEBUG
            Log.Print(LogType.Debug, "Regenerate...");
#endif

            if (_regenerationWorking)
            {
                Log.Print(LogType.Debug, "Update: Regenerator skipping update");
                return;
            }

            _regenerationWorking = true;

            try
            {
                foreach (var session in RealmServerSession.Sessions)
                {
                    if (session.Character == null)
                        continue;

                    if (session.Character.SubCurrentStats == null)
                        session.Character.SubCurrentStats = new SubStats
                        {
                            Life = session.Character.SubStats.Life,
                            Mana = session.Character.SubStats.ManaType != 1 ? session.Character.SubStats.Mana : 0
                        };

                    // TODO: not regen in combat

                    #region HEALTH Regen

                    if (session.Character.SubCurrentStats.Life < session.Character.SubStats.Life)
                    {
                        uint healh;
                        var healthModifier = 1;

                        // if sitting increase 33%
                        // http://wowwiki.wikia.com/wiki/Health_regeneration
                        if (session.Character.StandState == (int) StandStates.Sit)
                            healthModifier += 33;

                        if (session.Character.Race == Races.Troll)
                            healthModifier += 10;

                        switch (session.Character.Classe)
                        {
                            case Classes.Warrior:
                                healh = (uint) (session.Character.SubStats.Spirit * .80 + 6);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Paladin:
                                healh = (uint) (session.Character.SubStats.Spirit * .25 + 6);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Hunter:
                                healh = (uint) (session.Character.SubStats.Spirit * .25 + 6);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Rogue:
                                healh = (uint) (session.Character.SubStats.Spirit * .50 + 2);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Priest:
                                healh = (uint) (session.Character.SubStats.Spirit * .10 + 6);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Shaman:
                                healh = (uint) (session.Character.SubStats.Spirit * .11 + 7);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Mage:
                                healh = (uint) (session.Character.SubStats.Spirit * .10 + 6);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Warlock:
                                healh = (uint) (session.Character.SubStats.Spirit * .07 + 6); // 11
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            case Classes.Druid:
                                healh = (uint) (session.Character.SubStats.Spirit * .09 + 6.5);
                                healh = (uint) (healh + healh * healthModifier / 100);
                                break;
                            default:
                                healh = (uint) (session.Character.SubStats.Spirit * .8 + 6);
                                break;
                        }

                        session.Character.SubCurrentStats.Life += healh;
                        session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH,
                            session.Character.SubCurrentStats.Life);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Log.Print(LogType.Error, $"Error at regenerate.{ex.Message}");
            }
            finally
            {
                _regenerationWorking = false;
            }
        }
    }
}
