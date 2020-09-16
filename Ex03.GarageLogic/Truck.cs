using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class Truck: Vehicle
    {
        private bool m_DangerousMaterials;
        private float m_CargoVolume;

        public Truck(
            string i_CarModel,
            string i_LicenseNumber,
            float i_EnergyPercent,
            List<Wheel> i_Wheels,
            PowerUsage i_PowerSource,
            bool i_DangerousMaterials,
            float i_CargoVolume)
            : base(i_CarModel, i_LicenseNumber, i_EnergyPercent, i_Wheels, i_PowerSource)
        {
            m_CargoVolume = i_CargoVolume;
            m_DangerousMaterials = i_DangerousMaterials;
        }

        public static void GetDynamicParameter(Dictionary<string, Type> io_DynamicParams)
        {
            io_DynamicParams.Add("Dangerous Materials", typeof(bool));
            io_DynamicParams.Add("Cargo Volume", typeof(float));
        }

        public bool DangerousMaterials
        {
            get
            {
                return m_DangerousMaterials;
            }
            set
            {
                m_DangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                m_CargoVolume = value;
            }
        }

        public override string ToString()
        {
            return string.Format($@"{base.ToString()}
The information about the truck: 
The truck contains dangerous materials: {m_DangerousMaterials.ToString()}
The cargo volume is: {m_CargoVolume}
");
        }
    }
}
