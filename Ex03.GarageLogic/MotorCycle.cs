using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class MotorCycle: Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            B1,
            B2
        }

        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public MotorCycle(
            string i_CarModel,
            string i_LicenseNumber,
            float i_EnergyPercent,
            List<Wheel> i_Wheels,
            PowerUsage i_PowerSource,
            eLicenseType i_LicenseType,
            int i_EngineVolume)
            : base(i_CarModel, i_LicenseNumber, i_EnergyPercent, i_Wheels, i_PowerSource)
        {
            m_EngineVolume = i_EngineVolume;
            m_LicenseType = i_LicenseType;
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                m_EngineVolume = value;
            }
        }

        public override string ToString()
        {
            return string.Format($@"{base.ToString()} 
The information about the motorcycle:
The license type: {m_LicenseType};
The engine volume: {m_EngineVolume}
");
        }

        public static void GetDynamicParameter(Dictionary<string, Type> io_DynamicParams)
        {
            io_DynamicParams.Add("License type", typeof(eLicenseType));
            io_DynamicParams.Add("Engine volume", typeof(int));
        }

    }
}
