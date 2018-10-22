namespace RealmServer.Enums
{
    public enum TicketInfoResponse : uint
    {
        /// <summary>
        /// Error retrieving GM ticket.
        /// </summary>
        DDS = 0,
        Fail = 1,
        Saved = 2,
        A1 = 3,
        A2 = 4,
        A3 = 5,
        /// <summary>
        /// 
        /// </summary>
        Pending = 6,
        ve3 = 7,
        B1 = 8,
        Deleted = 9,
        NoTicket = 10
    }
}