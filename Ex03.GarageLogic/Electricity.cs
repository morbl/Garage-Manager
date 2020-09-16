

namespace Ex03.GarageLogic
{
    public class Electricity: PowerUsage
    {
        public Electricity(float i_MaxPower, float i_CurrentPower): base(i_MaxPower, i_CurrentPower){}
        public void AddPower(float i_AmountOfPowerToAdd)
        {
            if (i_AmountOfPowerToAdd + CurrentAmountOfPower > m_MaxAmountOfPower || i_AmountOfPowerToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxAmountOfPower);
            }

            CurrentAmountOfPower += i_AmountOfPowerToAdd;
        }

        public override string ToString()
        {
            return string.Format($@" 
There is currently {m_CurrentAmountOfPower} battery hours left
Maximum charging point is: {m_MaxAmountOfPower}
");
        }
    }
}
