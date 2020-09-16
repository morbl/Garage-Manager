using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        public enum ePowerType
        {
            Gas = 1,
            Electricity
        }
        
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType,string i_Model, string i_LicenseNumber, PowerUsage i_Power, List<Wheel> i_Wheels, Dictionary<string, object> i_DynamicParameters)

        {
            Vehicle vehicleOfOwner = null;
            float energyPercent = i_Power.CurrentAmountOfPower / i_Power.MaxAmountOfPower * 100;
            switch(i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicleOfOwner = new Car(i_Model,i_LicenseNumber,energyPercent,
                        i_Wheels,
                        i_Power,
                        (Car.eCarColor)Enum.GetValues(typeof(Car.eCarColor)).GetValue((int)i_DynamicParameters["Color"] - 1),
                        (Car.eNumberOfDoors)Enum.GetValues(typeof(Car.eNumberOfDoors)).GetValue((int)i_DynamicParameters["Number of doors"] - 1));
                    break;
                case eVehicleType.Motorcycle:
                    vehicleOfOwner = new MotorCycle(i_Model, i_LicenseNumber, energyPercent,
                        i_Wheels,
                        i_Power,
                        (MotorCycle.eLicenseType)Enum.GetValues(typeof(MotorCycle.eLicenseType)).GetValue((int)i_DynamicParameters["License type"] - 1),
                        (int)i_DynamicParameters["Engine volume"]);
                    break;
                case eVehicleType.Truck:
                    vehicleOfOwner = new Truck(i_Model, 
                        i_LicenseNumber,
                        energyPercent,
                        i_Wheels,
                        i_Power,
                        (bool)i_DynamicParameters["Dangerous Materials"],
                        (float)i_DynamicParameters["Cargo Volume"]);
                    break;
            }

            return vehicleOfOwner;

        }

        public static PowerUsage CreatePowerSource(
            eVehicleType i_VehicleType,
            ePowerType i_EnergyType,
            float i_AmountOfPowerSource)
        {
            PowerUsage powerSource = null;
            switch(i_VehicleType)
            {
                case eVehicleType.Car:
                    if(i_EnergyType == ePowerType.Gas)
                    {
                        powerSource = new Gas(Gas.eGasType.Octan96,i_AmountOfPowerSource,50f);
                    }
                    else
                    {
                        powerSource = new Electricity(1.6f,i_AmountOfPowerSource);
                    }

                    break;
                case eVehicleType.Motorcycle:
                    if (i_EnergyType == ePowerType.Gas)
                    {
                        powerSource = new Gas(Gas.eGasType.Octan95, i_AmountOfPowerSource, 5.5f);
                    }
                    else
                    {
                        powerSource = new Electricity(4.8f, i_AmountOfPowerSource);
                    }

                    break;
                case eVehicleType.Truck:
                    powerSource = new Gas(Gas.eGasType.Soler, i_AmountOfPowerSource, 105f);
                    break;
            }

            return powerSource;
        }

        public static List<Wheel> CreateWheels(eVehicleType i_VehicleType,string i_Manufacturer, float i_CurrAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>();
            int maxAirPressure;
            int amountOfWheels;

            if(i_VehicleType == eVehicleType.Car)
            {
                maxAirPressure = 32;
                amountOfWheels = 4;
            }
            else if(i_VehicleType == eVehicleType.Motorcycle)
            {
                maxAirPressure = 28;
                amountOfWheels = 2;
            }
            else
            {
                maxAirPressure = 16;
                amountOfWheels = 30;
            }

            for(int i = 0; i < amountOfWheels; i++)
            {
                wheels.Add(new Wheel(i_Manufacturer,i_CurrAirPressure,maxAirPressure));
            }

            return wheels;

        }

        public static VehicleInformation CreateVehicleWithFullInformation(Vehicle i_VehicleOfOwner, string i_OwnerName, string i_OwnerPhone)
        {
            return new VehicleInformation(i_OwnerName, i_OwnerPhone, i_VehicleOfOwner);
        }

        public static void GetRequiredVehicleParameters(int i_VehicleType, Dictionary<string, Type> io_DynamicParams)
        {
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;

            switch (vehicleType)
            {
                case eVehicleType.Car:
                    Car.GetDynamicParameter(io_DynamicParams);
                    break;
                case eVehicleType.Motorcycle:
                    MotorCycle.GetDynamicParameter(io_DynamicParams);
                    break;
                case eVehicleType.Truck:
                    Truck.GetDynamicParameter(io_DynamicParams);
                    break;
            }
        }

    }




}
