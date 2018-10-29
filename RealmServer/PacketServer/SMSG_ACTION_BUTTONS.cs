using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_ACTION_BUTTONS : Common.Network.PacketServer
    {
        public SMSG_ACTION_BUTTONS(Characters character) : base(RealmEnums.SMSG_ACTION_BUTTONS)
        {
            for (var button = 0; button < 120; button++) //  119    'or 480 ?
            {
                var subActionBar = character.SubActionBars.Find(x => x.Button == button);

                if (subActionBar != null)
                    Write((uint) subActionBar.Action | ((uint) subActionBar.Type << 24));
                else
                    Write((uint) 0);
            }
        }
    }
}
