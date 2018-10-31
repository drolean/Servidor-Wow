using System.Collections;
using System.Collections.Generic;
using Common.Database.Tables;
using RealmServer.PacketReader;

namespace RealmServer.World.Managers
{
    public class ChannelManager
    {
        private static List<Channel> _channels;

        public ChannelManager()
        {
            _channels = new List<Channel>();
        }

        internal static void OnJoin(RealmServerSession session, CMSG_JOIN_CHANNEL handler)
        {
            if (handler.Channel == null) return;

            Channel chan;

            if ((chan = ChanExists(handler.Channel)) != null)
            {
                chan.Join(session.Character);
            }
            else
            {
                chan = new Channel(handler.Channel, session.Character);
                _channels.Add(chan);
            }
        }

        internal static void OnLeave(RealmServerSession session, CMSG_LEAVE_CHANNEL handler)
        {
            if (handler.Channel == null) return;

            Channel chan;

            if ((chan = ChanExists(handler.Channel)) != null)
            {
                chan.Part(session.Character);
            }
        }

        internal static void OnList(RealmServerSession session, CMSG_LEAVE_CHANNEL handler)
        {
            if (handler.Channel == null) return;

            Channel chan;

            if ((chan = ChanExists(handler.Channel)) != null)
            {
                chan.List(session.Character);
            }
        }

        internal static void OnAnnouncment(RealmServerSession session, CMSG_CHANNEL_ANNOUNCEMENTS handler)
        {
            if (handler.ChannelName == null) return;
            
            Channel chan;

            if ((chan = ChanExists(handler.ChannelName)) != null)
            {
                if (chan.Announcment)
                {
                    chan.AnnouncmentOff(session.Character);
                }
                else
                {
                    chan.AnnouncmentOn(session.Character);
                }
            }
        }

        internal static void OnKick(RealmServerSession session, CMSG_CHANNEL_KICK handler)
        {
            if (handler.ChannelName == null || handler.ChannelUser == null) return;

            Channel chan;

            if ((chan = ChanExists(handler.ChannelName)) != null)
            {
                chan.Kick(session.Character, handler.ChannelUser);
            }
        }

        internal static void OnInvite(RealmServerSession session, CMSG_CHANNEL_INVITE handler)
        {
            if (handler.ChannelName == null) return;
            
            Channel chan;

            if ((chan = ChanExists(handler.ChannelName)) != null)
            {
                chan.Invite(session.Character, handler.ChannelUser);
            }
        }

        private static Channel ChanExists(string channel)
        {
            IEnumerator ienum = _channels.GetEnumerator();

            while (ienum.MoveNext())
            {
                if (((Channel)ienum.Current)?.Name == channel)
                    return (Channel) ienum.Current;
            }
            return null;
        }
    }

    public class Channel
    {
        public string Name;
        private string _channel;
        private Characters _character;
        public ArrayList Users;
        public bool Announcment;

        public Channel(string channel, Characters character)
        {
            _channel = channel;
            _character = character;
            Join(character);
        }

        public void Join(Characters character)
        {

        }

        public void Part(Characters sessionCharacter)
        {
            
        }

        public void List(Characters sessionCharacter)
        {
            
        }

        public void AnnouncmentOff(Characters sessionCharacter)
        {
            
        }

        public void AnnouncmentOn(Characters sessionCharacter)
        {
            
        }

        public void Invite(Characters sessionCharacter, string handlerChannelUser)
        {
            
        }

        public void Kick(Characters sessionCharacter, string handlerChannelUser)
        {
            
        }
    }
}
