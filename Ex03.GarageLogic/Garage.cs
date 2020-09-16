using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string,VehicleInformation> r_VehiclesToFix = new Dictionary<string, VehicleInformation>();

        public Dictionary<string, VehicleInformation> VehiclesToFix
        {
            get
            {
                return r_VehiclesToFix;
            }
        }

        public void ChangeStatusOfVehicle(string i_LicenseNumber, VehicleInformation.eVehicleCondition i_NewStatus)
        {
            r_VehiclesToFix[i_LicenseNumber].VehicleCondition = i_NewStatus;
        }

        public void AddMaxAirToWheelOfTheVehicle(string i_LicenseNumber)
        {
            foreach(Wheel wheel in r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.Wheels)
            {
                float amountToMaxAir = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.InflateWheel(amountToMaxAir);
            }
        }

        public bool IsVehicleExistByLicense(string i_LicenseNumber)
        {
            return r_VehiclesToFix.ContainsKey(i_LicenseNumber);
        }

        public string PrintSpecificVehicle(string i_LicenseNumber)
        {
            StringBuilder messageFromTheGarage = new StringBuilder();
            messageFromTheGarage.Append(r_VehiclesToFix[i_LicenseNumber].ToString());
            messageFromTheGarage.Append(Environment.NewLine);
            messageFromTheGarage.Append("===============================================");
           
            return messageFromTheGarage.ToString();
        }

        public void AddGas(string i_LicenseNumber, Gas.eGasType i_GasType, float i_AmountOfPowerToAdd)
        {
            Gas toRefill = r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.PowerSource as Gas;
            toRefill.AddPower(i_GasType, i_AmountOfPowerToAdd);
            r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.UpdatePercent();
        }

        public void AddElectricity(string i_LicenseNumber, float i_AmountOfPowerToAdd)
        {
            Electricity toRefill = r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.PowerSource as Electricity;
            toRefill.AddPower(i_AmountOfPowerToAdd);
            r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.UpdatePercent();
        }

        public void CanCarBeFueled(string i_LicenseNumber)
        {
            Gas powerOfCar = r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.PowerSource as Gas;
            if (powerOfCar == null)
            {
                throw new ArgumentException("Invalid type source the power source doesn't match the car");
            }
        }

        public void  CanCarBeCharged(string i_LicenseNumber)
        {
            Electricity powerOfCar = r_VehiclesToFix[i_LicenseNumber].VehicleOfOwner.PowerSource as Electricity;
            if(powerOfCar == null)
            {
                throw new ArgumentException("Invalid type source the power source doesn't match the car");
            }
        }

        public void AddToGarage(VehicleInformation i_NewVehicle)
        {
            r_VehiclesToFix.Add(i_NewVehicle.VehicleOfOwner.LicenseNumber,i_NewVehicle);
        }
    }
}
