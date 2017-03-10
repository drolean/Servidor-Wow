using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Database.Xml;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Game.Entitys
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
            _unitEntity.Move(session);
            //Console.WriteLine($@"Mapeamento andando [{_unitEntity.MapX} _ {_unitEntity.MapY} _ {_unitEntity.MapZ}]");
            // Definindo novo Map
            _unitEntity.MapX = session.Character.MapX;
            _unitEntity.MapY = session.Character.MapY;
            _unitEntity.MapZ = session.Character.MapZ;
        }
    }

    public class UnitEntity : ObjectEntity
    {
        public TypeId TypeId => TypeId.TypeidUnit;
        public override int DataLength => (int) UnitFields.UNIT_END - 0x4;

        public static int Ababa;
        public float MapX, MapY, MapZ, MapO;

        public UnitEntity(zoneObjeto zoneObjeto)
            : base(new ObjectGuid((uint) (zoneObjeto.id + Ababa), TypeId.TypeidUnit, HighGuid.HighguidUnit))
        {
            new AiBrain(this);

            Console.WriteLine($@" => {ObjectGuid.RawGuid}");

            //Definindo interno
            MapX = zoneObjeto.map.mapX;
            MapY = zoneObjeto.map.mapY;
            MapZ = zoneObjeto.map.mapZ;
            MapO = zoneObjeto.map.mapO;

            Type = 0x9;
            Scale = 0.42f;
            Entry = 30; // 30=Forest Spider

            //
            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, 382);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, 383);

            SetUpdateField((int) UnitFields.UNIT_NPC_FLAGS, 0);
            SetUpdateField((int) UnitFields.UNIT_DYNAMIC_FLAGS, 0);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, 0);

            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, 25);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, 60);
            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, 125);
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, 1);
        }

        public UnitEntity(ObjectGuid objectGuid) : base(objectGuid)
        {

        }

        public void Move(RealmServerSession session)
        {
            session.SendPacket(new SmsgMonsterMove(session, ObjectGuid.RawGuid, MapX, MapY, MapZ));
        }
    }

    internal sealed class SmsgMonsterMove : PacketServer
    {
        public SmsgMonsterMove(RealmServerSession session, ulong rawGuid, float mapX, float mapY, float mapZ)
            : base(RealmCMD.SMSG_MONSTER_MOVE)
        {
            this.WritePackedUInt64(rawGuid);
            Write(mapX);
            Write(mapY);
            Write(mapZ);
            Write((UInt32) Environment.TickCount);
            Write((byte) 0); // Type [If type is 1 then the packet ends here]
            Write(0); // Flags [0x0 - Walk, 0x100 - Run, 0x200 - Waypoint, 0x300 - Fly]
            Write(2000); // TIME aqui deve pegar o lag do client
            Write(1);
            Write(session.Character.MapX);
            Write(session.Character.MapY);
            Write(session.Character.MapZ);
        }
    }
}