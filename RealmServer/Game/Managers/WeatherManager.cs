using Common.Globals;
using Common.Network;

namespace RealmServer.Game.Managers
{
    public enum WeatherState
    {
        Fine = 0,
        Rain = 1,
        Snow = 2,
        Sandstorm = 3
    }

    public enum WeatherSounds
    {
        NoSound = 0,
        RainLight = 8533,
        RainMedium = 8534,
        RainHeavy = 8535,
        SnowLight = 8536,
        SnowMedium = 8537,
        SnowHeavy = 8538,
        SandStormLight = 8556,
        SandStormMedium = 8557,
        SandStormHeavy = 8558
    }

    sealed class Weather : PacketServer
    {
        public Weather(WeatherState state, float intensity, WeatherSounds sound) : base(RealmCMD.SMSG_WEATHER)
        {
            Write((uint) state);
            Write(intensity);
            Write((uint) sound);
        }

        public Weather() : base(RealmCMD.SMSG_WEATHER)
        {
            Write((uint) CurrentWeather);
            Write(Intensity);
            Write((uint) GetSound());
        }

        // definicoes
        public WeatherState CurrentWeather = WeatherState.Fine;
        public float Intensity = 0f;

        public WeatherSounds GetSound()
        {
            switch (CurrentWeather)
            {
                case WeatherState.Rain:
                    if (Intensity < 0.3333333f)
                        return WeatherSounds.RainLight;

                    return Intensity < 0.6666667f ? WeatherSounds.RainMedium : WeatherSounds.RainHeavy;
                case WeatherState.Snow:
                    if (Intensity < 0.3333333f)
                        return WeatherSounds.SnowLight;

                    return Intensity < 0.6666667f ? WeatherSounds.SnowMedium : WeatherSounds.SnowHeavy;

                case WeatherState.Sandstorm:
                    if (Intensity < 0.3333333f)
                        return WeatherSounds.SandStormLight;

                    return Intensity < 0.6666667f ? WeatherSounds.SandStormMedium : WeatherSounds.SandStormHeavy;

                default:
                    return WeatherSounds.NoSound;
            }
        }
    }
}
