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
                        $"[{character.Name}] Type: {subActionBar.Type} Button: {subActionBar.Button} " +
                        $"Action: {subActionBar.Action}");

                    UInt32 packedData = (UInt32) subActionBar.Action | (UInt32) subActionBar.Type << 24;
                    Write(packedData);
                }
                else
                {
                    Write((UInt32) 0);
                }

                /*
                int index = character.SubActionBars.FindIndex(b => b.button == button);

                CharactersActionBars currentButton = index != -1 ? savedButtons[index] : null;

                if (currentButton != null)
                {
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Action .: {currentButton.action}");
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Button .: {currentButton.button}");
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Type ...: {currentButton.type}");

                    UInt32 packedData = (UInt32)currentButton.action | (UInt32)currentButton.type << 24;
                    Write(packedData);
                    //Write((UInt16)currentButton.action);
                    //Write((int)currentButton.type);
                    //Write((int)currentButton.); ?? misc???
                }
                else
                    Write((UInt32)0);
                    */
            }
        }
    }
}