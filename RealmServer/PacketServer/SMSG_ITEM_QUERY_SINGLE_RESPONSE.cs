using System;
using System.Diagnostics;
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
                // Item ID
                Write(item.Entry);
                // Item Class
                Write(item.Class);
                // Item SubCLass
                Write(item.SubClass);
                // Item Name
                WriteCString(item.Name);
                // Item Name
                WriteCString(string.Empty);
                // Item Name
                WriteCString(string.Empty);
                // Item Name
                WriteCString(string.Empty);

                // Model Id
                Write(item.DisplayId);
                // Quality Id
                Write(item.Quality);

                // Flags
                Write(item.Flags);
                // Buy Price
                Write(item.BuyPrice);
                // Sell Price
                Write(item.SellPrice);
                // Inventory Type
                Write(item.InventoryType);

                // Req Class
                Write(item.AllowableClass);
                // Req Race
                Write(item.AllowableRace);

                // Level
                Write(item.ItemLevel);
                // Req Level
                Write(0);

                // Skill Req
                Write(0);
                // Skill Level Req
                Write(0);

                // Item Unique
                Write(0);
                // Max Stack
                Write(item.MaxCount);
                // Container Slots
                Write(0);

                for (int a = 0; a < 10; a++)
                {
                    Write(0); // Type
                    Write(0); // Value
                }

                for (int b = 0; b < 5; b++)
                {
                    Write(0); // Minimum Damage
                    Write(0); // Maximum Damage
                    Write(0); // Damage Type
                }

                Write(0); // Physical (Bonus) "Armor"
                Write(0); // Holy (BONUS)
                Write(0); // Fire (BONUS)
                Write(0); // Nature (BONUS)
                Write(0); // Frost (BONUS)
                Write(0); // Shadow (BONUS)

                Write(0); // Item Attack Speed.
                Write(0); // Ammo Type
                Write(0); // Max Durability ?? range modifier

                for (int c = 0; c < 5; c++) // Spell Info
                {
                    Write(0); // Category);
                    Write(0); // CategoryCoolDown);
                    Write(0); // Charges);
                    Write(-1); // Cooldown);
                    Write(0); // ID);
                    Write(-1); // Trigger);
                }

                Write(item.Bonding); // Bonding

                WriteCString(item.Description + ""); // Item Description

                Write(0); // PageText
                Write(0); // Language ID
                Write(0); // Page Material;
                Write(0); // Start Quest
                Write(0); // Lock
                Write(0); // Material
                Write(0); // SheAtTheType;
                //Write(0); // Unknown
                //Write(0); // Unknown 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }

}