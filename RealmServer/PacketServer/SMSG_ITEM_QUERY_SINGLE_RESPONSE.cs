using System;
using System.Diagnostics;
using System.Linq;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_ITEM_QUERY_SINGLE_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_ITEM_QUERY_SINGLE_RESPONSE(Items item) : base(RealmEnums.SMSG_ITEM_QUERY_SINGLE_RESPONSE)
        {
            try
            {
                Write(item.Entry);
                Write(item.Class);
                Write(item.SubClass);
                WriteCString(item.Name);
                WriteCString(string.Empty);
                WriteCString(string.Empty);
                WriteCString(string.Empty);
                Write(item.DisplayId);
                // Quality: [0] Poor [1] Common [2] Green [3] Rare [4] Epic [5] Legendary [6] Artifact/Heirloon
                Write(item.Quality);
                Write(item.Flags);

                Write(MainProgram.Vai); // ? Faction or BuyPrice
                Write(MainProgram.Vai); // ? BuyPrice or BuySell

                Write(item.InventoryType);
                Write(item.AllowableClass);
                Write(item.AllowableRace);

                Write(MainProgram.Vai); // ? LEVEL
                Write(item.SubRequired?.Level ?? 0);
                Write(item.SubRequired?.Skill ?? 0);
                Write(item.SubRequired?.SkillRank ?? 0);
                Write(item.SubRequired?.Spell ?? 0);
                Write(item.SubRequired?.HonorRank ?? 0);
                Write(item.SubRequired?.CityRank ?? 0);
                Write(item.SubRequired?.ReputationFaction ?? 0);
                Write(item.SubRequired?.ReputationRank ?? 0);

                Write(item.MaxCount);

                Write(MainProgram.Vai); //  ? Stackable
                Write(MainProgram.Vai); // ? ContainerSlots

                // SubStats
                for (int i = 0; i < 10; i++)
                {
                    if (item.SubStats != null && item.SubStats.Select((t, index) => index == i).First())
                    {
                        Write(item.SubStats[i].Value);
                        Write(item.SubStats[i].Type);
                    }
                    else
                    {
                        Write(0);
                        Write(0);
                    }
                }

                // SubDamage
                for (int i = 0; i < 5; i++)
                {
                    if (item.SubDamages != null && item.SubDamages.Select((t, index) => index == i).First())
                    {
                        Write((float) item.SubDamages[0].Min);
                        Write((float) item.SubDamages[0].Max);
                        Write((uint) item.SubDamages[0].Type);
                    }
                    else
                    {
                        Write((float) 0);
                        Write((float) 0);
                        Write((uint) 0);
                    }
                }
                
                // Resistences
                Write((uint) (item.SubResistences?.Armor ?? 0));
                Write((uint) MainProgram.Vai); // HOLY????
                Write((uint) (item.SubResistences?.Fire ?? 0));
                Write((uint) (item.SubResistences?.Nature ?? 0));
                Write((uint) (item.SubResistences?.Frost ?? 0));
                Write((uint) (item.SubResistences?.Shadow ?? 0));
                Write((uint) (item.SubResistences?.Arcane ?? 0));

                Write((uint) item.Delay);
                Write((uint) item.AmmoType);
                Write((float) MainProgram.Vai);  // rangedmod

                for (int i = 0; i < 5; i++)
                {
                    if (item.SubSpells != null && item.SubSpells.Select((t, index) => index == i).First())
                    {
                        Write(item.SubSpells[i].Id);
                        Write((uint) item.SubSpells[i].Trigger);
                        Write(item.SubSpells[i].Charges);
                        Write(item.SubSpells[i].CoolDown);
                        Write((uint) item.SubSpells[i].Category);
                        Write(item.SubSpells[i].CategoryCooldown);
                    }
                    else
                    {
                        Write(0);
                        Write((uint) 0);
                        Write(0);
                        Write(-1);
                        Write((uint) 0);
                        Write(-1);
                    }
                }

                Write((uint) item.Bonding);
                WriteCString(item.Description);
                Write((uint) item.PageText);
                Write((uint) item.LanguageId);
                Write((uint) item.PageMaterial);

                Write((uint) item.StartQuest);
                Write((uint) item.LockId);
                Write(item.Material);
                Write((uint) item.Sheath);
                Write(item.RandomProperty);
                Write((uint) item.Block);

                //Write((uint) MainProgram.Vai + 120);
                Write((uint) item.ItemSet);
                Write((uint) item.MaxDurability);

                Write((uint) item.Area);
                Write(item.Map);

                // TODO
                Write(item.BagFamily);

                Write(MainProgram.Vai); // IDK ????
                Write(MainProgram.Vai); // IDK ??

                Write(item.FoodType);

                Write(item.MinMoneyLoot);
                Write(item.MaxMoneyLoot);

                Write(MainProgram.Vai);
                Write(MainProgram.Vai);

                MainProgram.Vai++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}" +
                    $"{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }
}