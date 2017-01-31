using System;

namespace Framework.Helpers
{
    public static class Time
    {
        public static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static readonly long StartTime = UnixTimeMilliseconds;
        /// <summary>
        /// Gets the current Unix time.
        /// </summary>
        public static long UnixTime
        {
            get
            {
                var ts = (DateTime.UtcNow - UnixEpochStart);
                return (long)ts.TotalSeconds;
            }
        }

        /// <summary>
        /// Gets the current Unix time, in milliseconds.
        /// </summary>
        public static long UnixTimeMilliseconds
        {
            get
            {
                var ts = (DateTime.UtcNow - UnixEpochStart);
                return ts.ToMilliseconds();
            }
        }

        /// <summary>
        /// Converts a TimeSpan to its equivalent representation in milliseconds (Int64).
        /// </summary>
        /// <param name="span">The time span value to convert.</param>
        public static long ToMilliseconds(this TimeSpan span)
        {
            return (long)span.TotalMilliseconds;
        }

        /// <summary>
        /// Gets the system uptime.
        /// </summary>
        /// <returns>the system uptime in milliseconds</returns>
        public static uint GetSystemTime()
        {
            return (uint)Environment.TickCount;
        }

        public static uint getMSTime()
        {
            return (uint)(UnixTimeMilliseconds - StartTime);
        }

        public static uint getMSTimeDiff(uint oldMSTime, uint newMSTime)
        {
            if (oldMSTime > newMSTime)
                return (0xFFFFFFFF - oldMSTime) + newMSTime;
            else
                return newMSTime - oldMSTime;
        }

        public static uint getMSTimeDiffNow(uint oldMSTime)
        {
            var newMSTime = getMSTime();
            if (oldMSTime > newMSTime)
                return (0xFFFFFFFF - oldMSTime) + newMSTime;
            else
                return newMSTime - oldMSTime;
        }
    }

    public class TimeTrackerSmall
    {
        public TimeTrackerSmall(int expiry = 0)
        {
            i_expiryTime = expiry;
        }

        public void Update(int diff)
        {
            i_expiryTime -= diff;
        }

        public bool Passed()
        {
            return i_expiryTime <= 0;
        }

        public void Reset(int interval)
        {
            i_expiryTime = interval;
        }

        public int GetExpiry()
        {
            return i_expiryTime;
        }
        int i_expiryTime;
    }

    public class TimeTracker
    {
        public TimeTracker(int expiry)
        {
            i_expiryTime = expiry;
        }

        public void Update(int diff)
        {
            i_expiryTime -= diff;
        }

        public bool Passed()
        {
            return i_expiryTime <= 0;
        }

        public void Reset(int interval)
        {
            i_expiryTime = interval;
        }

        public int GetExpiry()
        {
            return i_expiryTime;
        }

        int i_expiryTime;
    }

    public class IntervalTimer
    {
        public IntervalTimer()
        {
            _interval = 0;
            _current = 0;
        }

        public void Update(int diff)
        {
            _current += diff;
            if (_current < 0)
                _current = 0;
        }

        public bool Passed()
        {
            return _current >= _interval;
        }

        public void Reset()
        {
            if (_current >= _interval)
                _current -= _interval;
        }

        public void SetCurrent(int current)
        {
            _current = current;
        }

        public void SetInterval(int interval)
        {
            _interval = interval;
        }

        public int GetInterval()
        {
            return _interval;
        }

        public int GetCurrent()
        {
            return _current;
        }

        int _interval;
        int _current;
    }

    public class PeriodicTimer
    {
        public PeriodicTimer(int period, int start_time)
        {
            i_period = period;
            i_expireTime = start_time;
        }

        public bool Update(int diff)
        {
            if ((i_expireTime -= diff) > 0)
                return false;

            i_expireTime += i_period > (int)diff ? i_period : diff;
            return true;
        }

        public void SetPeriodic(int period, int start_time)
        {
            i_expireTime = start_time;
            i_period = period;
        }

        // Tracker interface
        public void TUpdate(int diff) { i_expireTime -= diff; }
        public bool TPassed() { return i_expireTime <= 0; }
        public void TReset(int diff, int period) { i_expireTime += period > diff ? period : diff; }

        int i_period;
        int i_expireTime;
    }
}
