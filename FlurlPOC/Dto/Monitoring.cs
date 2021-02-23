

using System;

namespace FlurlPOC.Dto
{
    public class Monitoring
    {
        public DateTime started { get; set; }
        public DateTime finished { get; set; }
        public double rain { get; set; }
        public double temperatureMin { get; set; }
        public double temperatureMax { get; set; }
        public double temperatureInst { get; set; }
        public double humidityMin { get; set; }
        public double humidityMax { get; set; }
        public double humidityInst { get; set; }
        public double atmosphericPressureMin { get; set; }
        public double atmosphericPressureMax { get; set; }
        public double atmosphericPressureInst { get; set; }
        public double windSpeedInst { get; set; }
        public double windSpeedAverage { get; set; }
        public double windDirectionInst { get; set; }
        public double windDirectionAverage { get; set; }
        public double gustSpeed { get; set; }
        public double gustDirection { get; set; }
        public double sustainedGustSpeed { get; set; }
        public double sustainedGustDirection { get; set; }
        public double solarIrradiation { get; set; }

    }

}