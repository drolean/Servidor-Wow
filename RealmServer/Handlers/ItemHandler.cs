using System;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{
    #region SMSG_ITEM_QUERY_SINGLE_RESPONSE
    sealed class SmsgItemQuerySingleResponse : PacketServer
    {
        public SmsgItemQuerySingleResponse() : base(RealmCMD.SMSG_ITEM_QUERY_SINGLE_RESPONSE)
        {
            // Verifica dados do Item

            /*
            Write(item.Id); // 32
            Write(item.ObjectClass); //32
            Write(item.ObjectClass == ITEM_CLASS.ITEM_CLASS_CONSUMABLE ? 0 : item.SubClass); // 32

            WriteCString(item.Name); // string
            response.AddInt8(0); // 8 name2 ???
            response.AddInt8(0); // 8 name3 ???
            response.AddInt8(0); // 8 name4 ???

            response.AddInt32(item.Model); // 32
            response.AddInt32(item.Quality); // 32
            response.AddInt32(item.Flags); // 32
            response.AddInt32(item.BuyPrice); // 32
            response.AddInt32(item.SellPrice); // 32
            response.AddInt32(item.InventoryType); // 32
            response.AddUInt32(item.AvailableClasses); // Uint32
            response.AddUInt32(item.AvailableRaces); // Uint32
            response.AddInt32(item.Level); // 32
            response.AddInt32(item.ReqLevel); // 32
            response.AddInt32(item.ReqSkill); // 32
            response.AddInt32(item.ReqSkillRank); // 32
            response.AddInt32(item.ReqSpell); // 32
            response.AddInt32(item.ReqHonorRank); // 32
            response.AddInt32(item.ReqHonorRank2); // 32

            response.AddInt32(item.ReqFaction);          // RequiredReputationFaction
            response.AddInt32(item.ReqFactionLevel);     // RequiredRaputationRank
            response.AddInt32(item.Unique); // Was stackable
            response.AddInt32(item.Stackable);
            response.AddInt32(item.ContainerSlots);

            for (int i = 0; i <= 9; i++)
            {
                response.AddInt32(item.ItemBonusStatType(i));
                response.AddInt32(item.ItemBonusStatValue(i));
            }

            for (int i = 0; i <= 4; i++)
            {
                response.AddSingle(item.Damage(i).Minimum);
                response.AddSingle(item.Damage(i).Maximum);
                response.AddInt32(item.Damage(i).Type);
            }

            for (int i = 0; i <= 6; i++)
            {
                response.AddInt32(item.Resistances(i));
            }

            response.AddInt32(item.Delay);
            response.AddInt32(item.AmmoType);
            response.AddSingle(item.Range); //itemRangeModifier (Ranged Weapons = 100.0, Fishing Poles = 3.0)

            for (int i = 0; i <= 4; i++)
            {
                if (SPELLs.ContainsKey(item.Spells(i).SpellID) == false)
                {
                    response.AddInt32(0);
                    response.AddInt32(0);
                    response.AddInt32(0);
                    response.AddInt32(-1);
                    response.AddInt32(0);
                    response.AddInt32(-1);
                }
                else
                {
                    response.AddInt32(item.Spells(i).SpellID);
                    response.AddInt32(item.Spells(i).SpellTrigger);
                    response.AddInt32(item.Spells(i).SpellCharges);

                    if (item.Spells(i).SpellCooldown > 0 || item.Spells(i).SpellCategoryCooldown > 0)
                    {
                        response.AddInt32(item.Spells(i).SpellCooldown);
                        response.AddInt32(item.Spells(i).SpellCategory);
                        response.AddInt32(item.Spells(i).SpellCategoryCooldown);
                    }
                    else
                    {
                        response.AddInt32(SPELLs(item.Spells(i).SpellID).SpellCooldown);
                        response.AddInt32(SPELLs(item.Spells(i).SpellID).Category);
                        response.AddInt32(SPELLs(item.Spells(i).SpellID).CategoryCooldown);
                    }
                }
            }

            response.AddInt32(item.Bonding);
            response.AddString(item.Description);
            response.AddInt32(item.PageText);
            response.AddInt32(item.LanguageID);
            response.AddInt32(item.PageMaterial);
            response.AddInt32(item.StartQuest);
            response.AddInt32(item.LockID);
            response.AddInt32(item.Material);
            response.AddInt32(item.Sheath);
            response.AddInt32(item.Extra);
            response.AddInt32(item.Block);
            response.AddInt32(item.ItemSet);
            response.AddInt32(item.Durability);
            response.AddInt32(item.ZoneNameID);
            response.AddInt32(item.MapID);
            response.AddInt32(item.BagFamily); // Added in 1.12.1 client branch
            */
        }
    }
    #endregion

    internal class ItemHandler
    {
        internal static void OnItemQuerySingle(RealmServerSession session, PacketReader handler)
        {
            // check packet size[length
            uint itemId = handler.ReadUInt32();

            Log.Print(LogType.Debug, $"Checando item de id [{itemId}]");

            session.SendPacket(new SmsgItemQuerySingleResponse());
        }

        internal static void OnSwapInvItem(RealmServerSession session, PacketReader handler)
        {
            byte srcSlot = handler.ReadByte();
            byte dstSlot = handler.ReadByte();

            Log.Print(LogType.Debug, $"Trocando item de Slot [{srcSlot}] para ===> [{dstSlot}]");
        }

        internal static void OnDestroyItem(RealmServerSession session, PacketReader handler)
        {
            byte srcBag = handler.ReadByte();
            byte srcSlot = handler.ReadByte();
            byte count = handler.ReadByte();

            Log.Print(LogType.Debug, $"Destruindo item de Bag [{srcBag}] Slot: [{srcSlot}] Count: [{count}]");
        }

        internal static void OnUseItem(RealmServerSession session, PacketReader handler)
        {
            byte bag = handler.ReadByte();
            if (bag == 255) bag = 0;
            byte slot = handler.ReadByte();
            byte tmp = handler.ReadByte();

            Log.Print(LogType.Debug, $"Usando item [{bag}] Slot: [{slot}] Tmp: [{tmp}]");
        }

        internal static void OnAutoEquipItem(RealmServerSession session, PacketReader handler)
        {
            byte srcSlot = handler.ReadByte();
            byte dstSlot = handler.ReadByte();

            Log.Print(LogType.Debug, $"Auto Equipe item de Slot [{srcSlot}] para ===> [{dstSlot}]");
        }

        internal static void OnSplitItem(RealmServerSession session, PacketReader handler)
        {
            byte srcBag = handler.ReadByte();
            byte srcSlot = handler.ReadByte();
            byte dstBag = handler.ReadByte();
            byte dstSlot = handler.ReadByte();
            byte count = handler.ReadByte();
            if (dstBag == 255)
                dstBag = 0;
            if (srcBag == 255)
                srcBag = 0;

            Log.Print(LogType.Debug, $"Split Item srcSlot: [{srcSlot} => {dstSlot}] dstbag [{srcBag} => {dstBag}] count: {count}");
        }
    }
}