using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Objects
{
    public class BaseUnit : BaseObject
    {
        public float Size = 1.0f;

        public TStatBar Life = new TStatBar(1, 1, 0);
        public TStatBar Mana = new TStatBar(1, 1, 0);

        public byte Level = 0;
        public int Model  = 0;
        public int Mount  = 0;

        public int CUnitFlags = (int) UnitFlags.UNIT_FLAG_ATTACKABLE;       
        public int CDynamicFlags = 0;       // DynamicFlags.UNIT_DYNFLAG_SPECIALINFO

        public uint CBytes0 = 0;            // Race + Classe + Gender + ManaType
        public uint CBytes1 = 0;            // StandState + PetLoyalty + ShapeShift + StealthFlag
        public uint CBytes2 = 0xeeeeee00;   // ? ? ? ?

        public float BoundingRadius = 0.389f;
        public float CombatReach    = 1.5f;

        public int AttackPowerMods  = 0;

    }
}
