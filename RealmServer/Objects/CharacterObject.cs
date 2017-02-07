using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Objects
{
    public class CharacterObject : BaseUnit
    {
        public short Faction = (short)FactionTemplates.None; // FactionTemplate.dbc

        public TStatBar Rage   = new TStatBar(1, 1, 0);
        public TStatBar Energy = new TStatBar(1, 1, 0);
        public TStat Strength  = new TStat();
        public TStat Agility   = new TStat();
        public TStat Stamina   = new TStat();
        public TStat Intellect = new TStat();
        public TStat Spirit    = new TStat();

        public TDamage Damage  = new TDamage();

        public int ModelNative = 0;

        public int CPlayerBytes1 = 0;                   // Sking + Face + HairStyle + HairColor
        public int CPlayerBytes2 = 0x200ee00;           // FacialHair + ? + BanksSlotAvailable + RestState
        public int CPlayerBytes3 = 0;                   // Gender + Alcohol + Defense? + LastWeekHonorRank

        public uint CPlayerFieldBytes1 = 0xeee00000;    // ? + ComboPoints + ActionBar + HighestHonorRank
        public uint CPlayerFieldBytes2 = 0;             // HonorBar

        public byte WatchedFactionIndex = 0xff;

        public PlayerFlags CPlayerFlags = 0;

        // XP and Level Managment
        public int RestBonus = 0;
        public int XP = 0;

        // Spell/Skill/Talents System
        public byte TalentPoints = 0;

        // Guilds
        public uint GuildId = 0;
        public byte GuildRank = 0;
        public int GuildInvited = 0;
        public int GuildInvitedBy = 0;
        public bool IsInGuild => GuildId != 0;

        // Damage
        public int BaseUnarmedDamage => (int) ((AttackPower + AttackPowerMods) * 0.0714285714285714);
        public int AttackPower = 50; // <<<< REVIEW

        public short AttackTime(WeaponAttackType index)
        {
            return Fix(AttackTimeBase(index) * AttackTimeMods(index));
        }


        public short[] AttackTimeBase = { 2000, 0, 0 };
        public float[] AttackTimeMods = { 1f, 1f, 1f };

        public void FillAllUpdateFlags()
        {
            SetUpdateField((int) EObjectFields.OBJECT_FIELD_GUID, GUID);
            SetUpdateField((int) EObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) EObjectFields.OBJECT_FIELD_SCALE_X, Size);

//            if (Pet != null)
//                SetUpdateField(EUnitFields.UNIT_FIELD_SUMMON, Pet.GUID);

            SetUpdateField((int) EUnitFields.UNIT_FIELD_HEALTH, Life.Current);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_POWER1, Mana.Current);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_POWER2, Rage.Current);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_POWER4, Energy.Current);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_POWER5, 0);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER1, Mana.Maximum);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER2, Rage.Maximum);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER4, Energy.Maximum);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER5, 0);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_BASE_HEALTH, Life.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_BASE_MANA, Mana.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_LEVEL, Level);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_FACTIONTEMPLATE, Faction);

            SetUpdateField((int) EUnitFields.UNIT_FIELD_FLAGS, CUnitFlags);
            // SetUpdateField(EUnitFields.UNIT_FIELD_FLAGS_2, 0);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_STAT0, Strength.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_STAT1, Agility.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_STAT2, Stamina.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_STAT3, Spirit.Base);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_STAT4, Intellect.Base);

            SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_0, CBytes0);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_1, CBytes1);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_2, CBytes2);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MOUNTDISPLAYID, Mount);
            SetUpdateField((int) EUnitFields.UNIT_DYNAMIC_FLAGS, CDynamicFlags);

            SetUpdateField((int) EPlayerFields.PLAYER_BYTES, CPlayerBytes1);
            SetUpdateField((int) EPlayerFields.PLAYER_BYTES_2, CPlayerBytes2);
            SetUpdateField((int) EPlayerFields.PLAYER_BYTES_3, CPlayerBytes3);

            SetUpdateField((int) EPlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, WatchedFactionIndex);

            SetUpdateField((int) EPlayerFields.PLAYER_XP, XP);
            SetUpdateField((int) EPlayerFields.PLAYER_NEXT_LEVEL_XP, new int[60 + 1]);// XPTable(Level));
            SetUpdateField((int) EPlayerFields.PLAYER_REST_STATE_EXPERIENCE, RestBonus);

            SetUpdateField((int) EPlayerFields.PLAYER_FLAGS, CPlayerFlags);
            SetUpdateField((int) EPlayerFields.PLAYER_FIELD_BYTES, CPlayerFieldBytes1);
            SetUpdateField((int) EPlayerFields.PLAYER_FIELD_BYTES2, CPlayerFieldBytes2);

            SetUpdateField((int) EUnitFields.UNIT_FIELD_BOUNDINGRADIUS, BoundingRadius);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_COMBATREACH, CombatReach);

            SetUpdateField((int) EPlayerFields.PLAYER_CHARACTER_POINTS1, TalentPoints);
            // SetUpdateField(EPlayerFields.PLAYER_CHARACTER_POINTS2, 0);

            SetUpdateField((int) EPlayerFields.PLAYER_GUILDID, GuildId);
            SetUpdateField((int) EPlayerFields.PLAYER_GUILDRANK, GuildRank);

            SetUpdateField((int) EUnitFields.UNIT_FIELD_MINDAMAGE, Damage.Minimum);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXDAMAGE, Damage.Maximum + BaseUnarmedDamage);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_BASEATTACKTIME, AttackTime(WeaponAttackType.BASE_ATTACK));
            SetUpdateField((int) (EUnitFields.UNIT_FIELD_BASEATTACKTIME + 1), AttackTime(WeaponAttackType.OFF_ATTACK));
            SetUpdateField((int) EUnitFields.UNIT_MOD_CAST_SPEED, 1.0F);
            SetUpdateField((int) EUnitFields.UNIT_FIELD_ATTACK_POWER, AttackPower);
            SetUpdateField(EUnitFields.UNIT_FIELD_RANGED_ATTACK_POWER, AttackPowerRanged);
            SetUpdateField(EPlayerFields.PLAYER_CRIT_PERCENTAGE, GetBasePercentCrit(Me, 0));
            SetUpdateField(EPlayerFields.PLAYER_RANGED_CRIT_PERCENTAGE, GetBasePercentCrit(Me, 0));
            // SetUpdateField(EPlayerFields.PLAYER_FIELD_MOD_HEALING_DONE_POS, healing.PositiveBonus);

            for (byte i = 0; i <= 6; i++)
            {
                // SetUpdateField(EPlayerFields.PLAYER_SPELL_CRIT_PERCENTAGE1 + i, CType(0, Single))
                SetUpdateField(EPlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_POS + i, spellDamage(i).PositiveBonus);
                SetUpdateField(EPlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_NEG + i, spellDamage(i).NegativeBonus);
                SetUpdateField(EPlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_PCT + i, spellDamage(i).Modifier);
            }
        }
    }
}
