using System;


namespace Ex03.GarageLogic
{
    public class Gas: PowerUsage
    {
        public enum eGasType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler
        }

        private eGasType m_GasType;

        public Gas(eGasType i_GasType, float i_CurrentPower, float i_MaxPower)
            : base(i_MaxPower, i_CurrentPower)
        {
            m_GasType = i_GasType;
        }

        public void AddPower(eGasType i_GasType, float i_AmountOfPowerToAdd)
        {
            CheckGasType(i_GasType);
            if(i_AmountOfPowerToAdd + CurrentAmountOfPower > m_MaxAmountOfPower || i_AmountOfPowerToAdd < 0)
            {
                throw new ValueOutOfRangeException(0,m_MaxAmountOfPower);
            }

            CurrentAmountOfPower += i_AmountOfPowerToAdd;

        }

        public eGasType GasType
        {
            get
            {
                return m_GasType;
            }
            set
            {
                m_GasType = value;
            }
        }

        public override string ToString()
        {
            return string.Format($@" 
The fuel used: {m_GasType}
There is currently {m_CurrentAmountOfPower} liters left
Maximum capacity of fuel is {m_MaxAmountOfPower}
");
        }

        public void CheckGasType(eGasType i_GasType)
        {
            if(i_GasType != m_GasType)
            {
                throw new ArgumentException("Invalid gas type");
            }
        }

    }
}
