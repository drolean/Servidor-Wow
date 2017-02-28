namespace RealmServer.Game.Entitys
{
    internal class GameObject : ObjectEntity
    {
        public GameObject(ObjectGuid objectGuid) : base(objectGuid)
        {
        }

        public TypeId TypeId => TypeId.TypeidGameobject;
    }
}