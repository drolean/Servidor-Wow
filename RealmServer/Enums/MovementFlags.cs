using System;

namespace RealmServer.Enums
{
    [Flags]
    public enum MovementFlags
    {
        MoveflagNone = 0x00000000,
        MoveflagForward = 0x00000001,
        MoveflagBackward = 0x00000002,
        MoveflagStrafeLeft = 0x00000004,
        MoveflagStrafeRight = 0x00000008,
        MoveflagTurnLeft = 0x00000010,
        MoveflagTurnRight = 0x00000020,
        MoveflagPitchUp = 0x00000040,
        MoveflagPitchDown = 0x00000080,
        MoveflagWalkMode = 0x00000100, // Walking

        MoveflagLevitating = 0x00000400,
        MoveflagRoot = 0x00000800, // [-ZERO] is it really need and correct value
        MoveflagFalling = 0x00002000,
        MoveflagFallingfar = 0x00004000,
        MoveflagSwimming = 0x00200000, // appears with fly flag also
        MoveflagAscending = 0x00400000, // [-ZERO] is it really need and correct value
        MoveflagCanFly = 0x00800000, // [-ZERO] is it really need and correct value
        MoveflagFlying = 0x01000000, // [-ZERO] is it really need and correct value

        MoveflagOntransport = 0x02000000, // Used for flying on some creatures
        MoveflagSplineElevation = 0x04000000, // used for flight paths
        MoveflagSplineEnabled = 0x08000000, // used for flight paths
        MoveflagWaterwalking = 0x10000000, // prevent unit from falling through water
        MoveflagSafeFall = 0x20000000, // active rogue safe fall spell (passive)
        MoveflagHover = 0x40000000
    }
}