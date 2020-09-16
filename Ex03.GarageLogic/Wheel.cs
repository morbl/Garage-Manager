

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        public void InflateWheel(float i_AmountOfAir)
        {
            if(m_CurrentAirPressure + i_AmountOfAir > m_MaxAirPressure || i_AmountOfAir < 0)
            {
                throw new ValueOutOfRangeException(k_MinAirPressure, m_MaxAirPressure);
            }

            m_CurrentAirPressure += i_AmountOfAir;
        }

        public override string ToString()
        {
            return string.Format($@"The information about the wheel: 
The manufacturer is: {m_Manufacturer}
The current air pressure is {m_CurrentAirPressure}
The max air pressure is {m_MaxAirPressure}");
        }
    }
}
