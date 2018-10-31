using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RealmServer.World.Enititys;

namespace RealmServer.World
{
    public class AiBrain
    {
        private readonly UnitEntity _unitEntity;

        public static List<AiBrain> AiBrains = new List<AiBrain>();

        public AiBrain(UnitEntity unitEntity)
        {
            _unitEntity = unitEntity;
            AiBrains.Add(this);
        }

        public static void Boot()
        {
            new Thread(UpdateThread).Start();
        }

        private static void UpdateThread()
        {
            while (true)
            {
                UpdateVerify();
                Thread.Sleep(500);
            }
        }

        private static void UpdateVerify()
        {
            AiBrains.ForEach(ai => ai.Update());
        }

        private void Update()
        {
            RealmServerSession session = RealmServerSession.Sessions.First();
            //_unitEntity.Move(session);
            //_unitEntity.MapX = session.Character.SubMap.MapX;
            //_unitEntity.MapY = session.Character.SubMap.MapY;
            //_unitEntity.MapZ = session.Character.SubMap.MapZ;
        }
    }

}
