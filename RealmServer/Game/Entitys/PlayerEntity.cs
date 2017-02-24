using Common.Database.Tables;
using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public override int DataLength => (int)UnitFields.UNIT_END - 0x4;

        public int CUnitFlags    = (int) UnitFlags.UNIT_FLAG_ATTACKABLE;
        public int CDynamicFlags = 0;

        //                       <<0    <<8    <<16    <<24
        public uint CBytes0 = 0; // Race +       Classe +     Gender +     ManaType
        public uint CBytes1 = 0; // StandState + PetLoyalty + ShapeShift + StealthFlag [CType(Invisibility > InvisibilityLevel.VISIBLE, Integer) * 2 << 24]
        public uint CBytes2 = 0xeeeeee00; // ?    ?    ?    ?

        public float BoundingRadius = 0.389f;
        public float CombatReach = 1.5f;
    }

    public class PlayerEntity : UnitEntity
    {
        public override int DataLength => (int)PlayerFields.PLAYER_END - 0x4;

        public StatBar Rage   = new StatBar(1, 1, 0);
        public StatBar Energy = new StatBar(1, 1, 0);

        public static Stat Strength  = new Stat();
        public Stat Agility   = new Stat();
        public Stat Stamina   = new Stat();
        public Stat Intellect = new Stat();
        public Stat Spirit    = new Stat();

        // [ ] Character Information
        public int ModelNative;


        public PlayerEntity(Characters character) : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            /* Definindo Character */
            Model = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            ModelNative = (int)CharacterHelper.GetRaceModel(character.race, character.gender);
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);
     
            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, Life.Current);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER1, Mana.Current);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER2, Rage.Current);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER4, Energy.Current);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_POWER5, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER1, Mana.Maximum);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER2, Rage.Maximum);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER4, Energy.Maximum);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MAXPOWER5, 0);

            //SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_HEALTH, Life.Base);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_BASE_MANA, Mana.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, Faction);
            /*
            //SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, CUnitFlags);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, Strength.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, Agility.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, Stamina.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, Spirit.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, Intellect.Base);
            */
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.race);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.classe, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.gender, 2);
            
            //SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, CBytes1);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, CBytes2);
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);
            //SetUpdateField((int) UnitFields.UNIT_FIELD_MOUNTDISPLAYID, Mount);
            //SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, CDynamicFlags);
            
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_skin);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_face, 1);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairStyle, 2);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairColor, 3);
            //SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, CPlayerBytes2);
            //SetUpdateField((int) PlayerFields.PLAYER_BYTES_3, CPlayerBytes3);
            /*
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, WatchedFactionIndex);

            SetUpdateField((int) PlayerFields.PLAYER_XP, Xp);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, XpTable);
            SetUpdateField((int) PlayerFields.PLAYER_REST_STATE_EXPERIENCE, RestBonus);
            
            SetUpdateField((int) PlayerFields.PLAYER_FLAGS, CPlayerFlags);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_BYTES, CPlayerFieldBytes);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_BYTES2, CPlayerFieldBytes2);

            SetUpdateField((int) UnitFields.UNIT_FIELD_BOUNDINGRADIUS, BoundingRadius);
            SetUpdateField((int) UnitFields.UNIT_FIELD_COMBATREACH, CombatReach);

            SetUpdateField((int) PlayerFields.PLAYER_CHARACTER_POINTS1, TalentPoints);
            //SetUpdateFlag(EPlayerFields.PLAYER_CHARACTER_POINTS2, 0)

            SetUpdateField((int) PlayerFields.PLAYER_GUILDID, GuildId);
            SetUpdateField((int) PlayerFields.PLAYER_GUILDRANK, GuildRank);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MINDAMAGE, Damage.Minimum);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXDAMAGE, Damage.Maximum + BaseUnarmedDamage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BASEATTACKTIME, AttackTime(WeaponAttackType.BaseAttack));
            SetUpdateField((int) (UnitFields.UNIT_FIELD_BASEATTACKTIME + 1), AttackTime(WeaponAttackType.OffAttack));
            SetUpdateField((int) UnitFields.UNIT_MOD_CAST_SPEED, 1.0F);
            SetUpdateField((int) UnitFields.UNIT_FIELD_ATTACK_POWER, AttackPower);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RANGED_ATTACK_POWER, AttackPowerRanged);
            SetUpdateField((int) PlayerFields.PLAYER_CRIT_PERCENTAGE, GetBasePercentCrit());
            SetUpdateField((int) PlayerFields.PLAYER_RANGED_CRIT_PERCENTAGE, GetBasePercentCrit());
            //SetUpdateFlag(EPlayerFields.PLAYER_FIELD_MOD_HEALING_DONE_POS, healing.PositiveBonus)
            /*
            for (byte i = 0; i <= 6; i++)
            {
                //SetUpdateFlag(EPlayerFields.PLAYER_SPELL_CRIT_PERCENTAGE1 + i, CType(0, Single))
                SetUpdateField((int) (PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_POS + i), SpellDamage[i].PositiveBonus);
                SetUpdateField((int) (PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_NEG + i), SpellDamage[i].NegativeBonus);
                SetUpdateField((int) (PlayerFields.PLAYER_FIELD_MOD_DAMAGE_DONE_PCT + i), SpellDamage[i].Modifier);
            }

            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgPhysical), Resistances[(int) DamageTypes.DmgPhysical].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgHoly), Resistances[(int) DamageTypes.DmgHoly].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgFire), Resistances[(int) DamageTypes.DmgFire].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgNature), Resistances[(int) DamageTypes.DmgNature].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgFrost), Resistances[(int) DamageTypes.DmgFrost].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgShadow), Resistances[(int) DamageTypes.DmgShadow].Base);
            SetUpdateField((int) (UnitFields.UNIT_FIELD_RESISTANCES + (int) DamageTypes.DmgArcane), Resistances[(int) DamageTypes.DmgArcane].Base);
            *****

            SetUpdateField((int) PlayerFields.PLAYER_FIELD_COINAGE, Copper);

            int skillCount = 0;
            foreach (CharactersSkills skill in MainForm.Database.GetSkills(character))
            {
                SetUpdateField((int) (PlayerFields.PLAYER_SKILL_INFO_1_1 + skillCount * 3), skill.skill);
                SetUpdateField((int) (PlayerFields.PLAYER_SKILL_INFO_1_1 + skillCount * 3 + 1), skill.value + (skill.max << 16));
                //SetUpdateField(PlayerFields.PLAYER_SKILL_INFO_1_1 + SkillsPositions(skill.Key) * 3 + 2, skill.Value.Bonus);
                skillCount++;
            }

            SetUpdateField((int) UnitFields.UNIT_FIELD_RANGEDATTACKTIME, AttackTime(WeaponAttackType.RangedAttack));
            SetUpdateField((int) UnitFields.UNIT_FIELD_MINOFFHANDDAMAGE, OffHandDamage.Minimum);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXOFFHANDDAMAGE, OffHandDamage.Maximum);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT0, Strength.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT1, Agility.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT2, Stamina.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT3, Spirit.Base);
            SetUpdateField((int) UnitFields.UNIT_FIELD_STAT4, Intellect.Base);

            SetUpdateField((int) UnitFields.UNIT_FIELD_ATTACK_POWER_MODS, AttackPowerMods);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, AttackPowerModsRanged);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MINRANGEDDAMAGE, RangedDamage.Minimum);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXRANGEDDAMAGE, RangedDamage.Maximum + BaseRangedDamage);
            SetUpdateField((int) UnitFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER, 0.0F);
            SetUpdateField((int) UnitFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER, 0.0F);

            // QUESTS 

            SetUpdateField((int) PlayerFields.PLAYER_BLOCK_PERCENTAGE, GetBasePercentBlock());
            SetUpdateField((int) PlayerFields.PLAYER_DODGE_PERCENTAGE, GetBasePercentDodge());
            SetUpdateField((int) PlayerFields.PLAYER_PARRY_PERCENTAGE, GetBasePercentParry());

            for (byte i = 0; i <= 64-1; i++)
            {
                SetUpdateField((int) (PlayerFields.PLAYER_EXPLORED_ZONES_1 + i), ZonesExplored[i]);
            }

            //SetUpdateField(PlayerFields.PLAYER_FIELD_LIFETIME_HONORBALE_KILLS, HonorKillsLifeTime);
            //SetUpdateField(PlayerFields.PLAYER_FIELD_LIFETIME_DISHONORBALE_KILLS, DishonorKillsLifeTime);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_SESSION_KILLS, HonorKillsToday + (Convert.ToInt32(DishonorKillsToday) << 16));
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_THIS_WEEK_KILLS, HonorKillsThisWeek);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_LAST_WEEK_KILLS, HonorKillsLastWeek);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_YESTERDAY_KILLS, HonorKillsYesterday);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_THIS_WEEK_CONTRIBUTION, HonorPointsThisWeek);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_LAST_WEEK_CONTRIBUTION, HonorPointsLastWeek);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION, HonorPointsYesterday);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_LAST_WEEK_RANK, StandingLastWeek);

            var inventory = MainForm.Database.GetInventory(character);

            for (int i = (int) EquipmentSlots.EQUIPMENT_SLOT_START; i <= (int) (KeyRingSlots.KEYRING_SLOT_END - 1); i++)
            {
                if (inventory.Find(item => item.slot == i) != null)
                {
                    if (i < (int) EquipmentSlots.EQUIPMENT_SLOT_END)
                    {
                        SetUpdateField((int) (PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + (i * PlayerVisibleItemSize)), inventory.Find(item => item.slot == i).item);
                        // Include enchantment info
                        SetUpdateField((int) (PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + i * PlayerVisibleItemSize), 0);
                    }
                    SetUpdateField((int) (PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 2), inventory.Find(item => item.slot == i).Id);
                }
                else
                {
                    if (i < (int) EquipmentSlots.EQUIPMENT_SLOT_END)
                    {
                        SetUpdateField((int) (PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + i * PlayerVisibleItemSize), 0);
                        SetUpdateField((int) (PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + i * PlayerVisibleItemSize), 0);
                    }
                    SetUpdateField((int) (PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + i * 2), 0);
                }
            }

            SetUpdateField((int) PlayerFields.PLAYER_AMMO_ID, AmmoId);
            */
        }
    }
}
