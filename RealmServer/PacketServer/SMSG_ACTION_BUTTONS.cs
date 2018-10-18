using System;
using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_ACTION_BUTTONS : Common.Network.PacketServer
    {
        public SMSG_ACTION_BUTTONS(Common.Database.Tables.Characters character) : base(RealmEnums.SMSG_ACTION_BUTTONS)
        {
            for (int button = 0; button < 120; button++) //  119    'or 480 ?
            {
                var subActionBar = character.SubActionBars.Find(x => x.Button == button);

                if (subActionBar != null)
                {
                    Log.Print(LogType.Debug,
                        $"[{character.Name}] Button Number: {subActionBar.Button} - Type: {subActionBar.Type} " +
                        $"Action: {subActionBar.Action}");

                    UInt32 packedData = (UInt32) subActionBar.Action | (UInt32) subActionBar.Type << 24;
                    Write(packedData);
                }
                else
                {
                    Write((UInt32) 0);
                }
            }
        }
    }
}