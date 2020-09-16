
namespace Ex03.GarageLogic
{
    public abstract class PowerUsage
    {
        protected float m_MaxAmountOfPower;
        protected float m_CurrentAmountOfPower;

        public PowerUsage(float i_MaxAmountOfPower, float i_CurrentAmountOfPower)
        {
            m_MaxAmountOfPower = i_MaxAmountOfPower;
            m_CurrentAmountOfPower = i_CurrentAmountOfPower;
        }

        public float MaxAmountOfPower
        {
            get
            {
                return m_MaxAmountOfPower;
            }
            set
            {
                m_MaxAmountOfPower = value;
            }
        }

        public float CurrentAmountOfPower
        {
            get
            {
                return m_CurrentAmountOfPower;
            }
            set
            {
                m_CurrentAmountOfPower = value;
            }
        }


        public abstract override string ToString();

    }
}
