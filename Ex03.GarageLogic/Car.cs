using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class Car: Vehicle
    {
        public enum eCarColor
        {
            Gray,
            White,
            Green,
            Red
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(
            string i_CarModel,
            string i_LicenseNumber,
            float i_EnergyPercent,
            List<Wheel> i_Wheels,
            PowerUsage i_PowerSource,
            eCarColor i_CarColor,
            eNumberOfDoors i_NumberOfDoors)
            : base(i_CarModel, i_LicenseNumber, i_EnergyPercent, i_Wheels, i_PowerSource)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }
        
        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                m_NumberOfDoors = value;
            }
        }

        public override string ToString()
        {
            return string.Format($@"{base.ToString()}
The information about the car: 
The color of the car is: {m_CarColor}
The number of the doors is {m_NumberOfDoors}
");
        }

        public static void GetDynamicParameter(Dictionary<string, Type> io_DynamicParams)
        {
            io_DynamicParams.Add("Color", typeof(eCarColor));
            io_DynamicParams.Add("Number of doors", typeof(eNumberOfDoors));
        }

    }
}
