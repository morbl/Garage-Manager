using System;


namespace Ex03.GarageLogic
{
    public class VehicleInformation
    {
        private string m_OwnerName;
        private string m_OwnerTelephone;
        private Vehicle m_Vehicle;
        private eVehicleCondition m_VehicleCondition;

        public VehicleInformation(string i_OwnerName,
        string i_OwnerTelephone,
        Vehicle i_Vehicle)
        { 
            m_OwnerName = i_OwnerName;
            m_OwnerTelephone = i_OwnerTelephone;
            m_Vehicle = i_Vehicle;
            m_VehicleCondition = eVehicleCondition.Repair;
        }

        public enum eVehicleCondition
        {
            Repair = 1,
            Fixed,
            Paid
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerTelephone
        {
            get
            {
                return m_OwnerTelephone;
            }
            set
            {
                m_OwnerTelephone = value;
            }
        }

        public Vehicle VehicleOfOwner
        {
            get
            {
                return m_Vehicle;
            }
            set
            {
                m_Vehicle = value;
            }
        }

        public eVehicleCondition VehicleCondition
        {
            get
            {
                return m_VehicleCondition;
            }
            set
            {
                m_VehicleCondition = value;
            }
        }

        public override string ToString()
        {
            return String.Format(
                $@"
The owner's name is: {m_OwnerName} 
The owner's number is: {m_OwnerTelephone}    
The vehicle information is: 
{m_Vehicle.ToString()}
The condition of the vehicle: {m_VehicleCondition.ToString()}");

        }
    }
}
