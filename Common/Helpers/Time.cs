using System;

namespace Common.Helpers
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
                var ts = DateTime.UtcNow - UnixEpochStart;
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
                var ts = DateTime.UtcNow - UnixEpochStart;
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

        public static uint GetMsTime()
        {
            return (uint)(UnixTimeMilliseconds - StartTime);
        }

        public static uint GetMsTimeDiff(uint oldMsTime, uint newMsTime)
        {
            if (oldMsTime > newMsTime)
                return 0xFFFFFFFF - oldMsTime + newMsTime;
            return newMsTime - oldMsTime;
        }

        public static uint GetMsTimeDiffNow(uint oldMsTime)
        {
            var newMsTime = GetMsTime();
            if (oldMsTime > newMsTime)
                return 0xFFFFFFFF - oldMsTime + newMsTime;
            return newMsTime - oldMsTime;
        }
    }

    public class TimeTrackerSmall
    {
        public TimeTrackerSmall(int expiry = 0)
        {
            _iExpiryTime = expiry;
        }

        public void Update(int diff)
        {
            _iExpiryTime -= diff;
        }

        public bool Passed()
        {
            return _iExpiryTime <= 0;
        }

        public void Reset(int interval)
        {
            _iExpiryTime = interval;
        }

        public int GetExpiry()
        {
            return _iExpiryTime;
        }
        int _iExpiryTime;
    }

    public class TimeTracker
    {
        public TimeTracker(int expiry)
        {
            _iExpiryTime = expiry;
        }

        public void Update(int diff)
        {
            _iExpiryTime -= diff;
        }

        public bool Passed()
        {
            return _iExpiryTime <= 0;
        }

        public void Reset(int interval)
        {
            _iExpiryTime = interval;
        }

        public int GetExpiry()
        {
            return _iExpiryTime;
        }

        int _iExpiryTime;
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
        public PeriodicTimer(int period, int startTime)
        {
            _iPeriod = period;
            _iExpireTime = startTime;
        }

        public bool Update(int diff)
        {
            if ((_iExpireTime -= diff) > 0)
                return false;

            _iExpireTime += _iPeriod > diff ? _iPeriod : diff;
            return true;
        }

        public void SetPeriodic(int period, int startTime)
        {
            _iExpireTime = startTime;
            _iPeriod = period;
        }

        // Tracker interface
        public bool Passed() { return _iExpireTime <= 0; }
        public void Reset(int diff, int period) { _iExpireTime += period > diff ? period : diff; }

        int _iPeriod;
        int _iExpireTime;
    }
}
