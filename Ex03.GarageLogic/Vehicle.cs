using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_CarModel;
        protected readonly string r_LicenseNumber;
        protected float m_EnergyPercent;
        protected List<Wheel> m_Wheels;
        protected PowerUsage m_PowerSource;

        public Vehicle(
            string i_CarModel,
            string i_LicenseNumber,
            float i_EnergyPercent,
            List<Wheel> i_Wheels,
            PowerUsage i_PowerSource)
        {
            m_CarModel = i_CarModel;
            r_LicenseNumber = i_LicenseNumber;
            m_EnergyPercent = i_EnergyPercent;
            m_Wheels = i_Wheels;
            m_PowerSource = i_PowerSource;
        }

        public string CarModel
        {
            get
            {
                return m_CarModel;
            }
            set
            {
                m_CarModel = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }

        }

        public float EnergyPercent
        {
            get
            {
                return m_EnergyPercent;
            }
            set
            {
                m_EnergyPercent = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                m_Wheels = value;
            }
        }

        public PowerUsage PowerSource
        {
            get
            {
                return m_PowerSource;
            }
            set
            {
                m_PowerSource = value;
            }
        }


        public override string ToString()
        {
            return string.Format(
                $@"
The model is: {m_CarModel} 
The license number is: {r_LicenseNumber}    
The energy percent is: {m_EnergyPercent}
{m_Wheels[0].ToString()}
{m_PowerSource.ToString()}");
        }

        public void UpdatePercent()
        {
            m_EnergyPercent = (m_PowerSource.CurrentAmountOfPower / m_PowerSource.MaxAmountOfPower) * 100;
        }
    }

}
