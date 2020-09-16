using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public const int k_ExitFromMenu = 8;
        public const int k_FirstOptionInMenu = 1;

        public enum eMenuChoice
        {
            One = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight
        }

        public static void StartMenu()
        {
           
            string userInputOfChoice;
            int choiceOfUser;
            Garage garage = new Garage();
            bool continueShowingMenu = true;
            while(continueShowingMenu == true)
            {
                try
                {
                    PrintMenu();
                    userInputOfChoice = Console.ReadLine();
                    bool isParsingWorked = int.TryParse(userInputOfChoice, out choiceOfUser);
                    CheckUserChoice(choiceOfUser, isParsingWorked);

                    if(choiceOfUser == k_ExitFromMenu)
                    {
                        PrintExit();
                        continueShowingMenu = false;
                    }

                    else
                    {
                        DoUserChoice(garage, choiceOfUser);
                    }


                }

                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        public static void CheckUserChoice(int i_ChoiceOfUser, bool i_ParsingWorked)
        {
            if(i_ChoiceOfUser < k_FirstOptionInMenu || i_ChoiceOfUser > k_ExitFromMenu || i_ParsingWorked == false)
            {
                throw new FormatException("Invalid option please try again");
            }
        }

        public static void PrintMenu()
        {
            PrintLine();
            string messageMenu = String.Format(
                @"Hello, welcome to our garage. 
Thank you for choosing 'Mor&Lia' for your car. 
We promise you it is the best for you!
We are going to show you our options, please make a choice: 

1 - Add your vehicle to the garage.
2 - Show all license plates of all vehicles in the garage.
3 - Change status of a specific vehicle in the garage.
4 - Inflate tires of specific vehicle in the garage.
5 - Add gas to a vehicle powered by fuel.
6 - Charge a vehicle powered by electricity.
7 - Show all details of a specific vehicle.
8 - Exit from the menu.
");
            Console.WriteLine(messageMenu);
        }

        public static void PrintExit()
        {
            Console.WriteLine("Thank you for using our garage system. We hope you had a great experience with us.");
        }

        public static void DoUserChoice(Garage i_Garage, int i_Choice)
        {
            switch((eMenuChoice)i_Choice)
            {
                case eMenuChoice.One:
                    EnterVehicleToGarage(i_Garage);
                    break;
                case eMenuChoice.Two:
                    ShowAllLicenseNumbers(i_Garage);
                    break;
                case eMenuChoice.Three:
                    ChangeVehicleStatus(i_Garage);
                    break;
                case eMenuChoice.Four:
                    AddAirToWheels(i_Garage);
                    break;
                case eMenuChoice.Five:
                    AddGasToVehicle(i_Garage);
                    break;
                case eMenuChoice.Six:
                    AddElectricityToCar(i_Garage);
                    break;
                case eMenuChoice.Seven:
                    PrintCarByLicenseNumber(i_Garage);
                    break;
            }
        }

        public static void EnterVehicleToGarage(Garage i_Garage)
        {
            bool askAgain = true;
            while(askAgain == true)
            {
                try
                {
                    Console.WriteLine(@"Please Enter license number of the car");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);

                    if(i_Garage.IsVehicleExistByLicense(licenseNumber) == true)
                    {
                        i_Garage.ChangeStatusOfVehicle(licenseNumber, VehicleInformation.eVehicleCondition.Repair);
                        Console.WriteLine("This vehicle exist in our system it has been moved to in repair.");
                    }
                    else
                    {
                        EnterNewVehicleToGarage(i_Garage, licenseNumber);

                    }

                    askAgain = false;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

        public static void EnterNewVehicleToGarage(Garage i_Garage, string i_LicenseNumber)
        {
            Vehicle vehicle = CreateNewVehicle(i_LicenseNumber);
            string ownerName = GetOwnerName();
            string ownerPhone = GetOwnerPhone();

            VehicleInformation newVehicleInGarage = VehicleCreator.CreateVehicleWithFullInformation(vehicle, ownerName, ownerPhone);
            i_Garage.AddToGarage(newVehicleInGarage);
        }

        public static string GetOwnerPhone()
        {
            bool askAgain = true;
            string ownerPhone = null;
            long phoneInInt;

            while (askAgain == true)
            {
                Console.WriteLine("Enter owner's phone");
                ownerPhone = Console.ReadLine();

                if(ownerPhone.Length == 10 && long.TryParse(ownerPhone,out phoneInInt))
                {
                    askAgain = false;
                }

                else
                {
                    Console.WriteLine("Wrong format please try again.");
                }
            }

            return ownerPhone;
        }

        public static string GetOwnerName()
        {
            Console.WriteLine("Enter owner's name");
            string ownerName = Console.ReadLine();
            return ownerName;
        }

        public static Vehicle CreateNewVehicle(string i_LicenseNumber)
        {
            int energyType = 1;
            Dictionary<string, Type> dynamicParams = new Dictionary<string, Type>();
            Dictionary<string, object> dynamicObject = new Dictionary<string, object>();

            Console.WriteLine(@"Please Enter your type of vehicle");
            int vehicleType = getEnumFromUser(typeof(VehicleCreator.eVehicleType));
            if(vehicleType == 1 || vehicleType == 2)
            {
                Console.WriteLine(@"Please Enter your energy source:");
                energyType = getEnumFromUser(typeof(VehicleCreator.ePowerType));
            }

            Console.WriteLine(@"Please Enter your model of vehicle");
            string modelOfVehicle = Console.ReadLine();

            float amountOfPowerSource = GetAmountOfPowerSource((VehicleCreator.eVehicleType)vehicleType,
                (VehicleCreator.ePowerType)energyType);

            PowerUsage power = VehicleCreator.CreatePowerSource(
                (VehicleCreator.eVehicleType)vehicleType,
                (VehicleCreator.ePowerType)energyType,
                amountOfPowerSource);

            Console.WriteLine("Please enter the manufacturer of the wheels");
            string wheelsManufacturer = Console.ReadLine();

            float currAirPressure = GetAirPressure((VehicleCreator.eVehicleType)vehicleType,
                (VehicleCreator.ePowerType)energyType); 

            List<Wheel> vehicleWheels = VehicleCreator.CreateWheels(
                (VehicleCreator.eVehicleType)vehicleType,
                wheelsManufacturer,
                currAirPressure);

            VehicleCreator.GetRequiredVehicleParameters(vehicleType, dynamicParams);

            getDynamicParametersDataFromUser(dynamicParams, dynamicObject);
            Vehicle newVehicle = VehicleCreator.CreateVehicle((VehicleCreator.eVehicleType)vehicleType, modelOfVehicle,i_LicenseNumber, power,vehicleWheels,dynamicObject);

            return newVehicle;
        }

        public static float GetAmountOfPowerSource(VehicleCreator.eVehicleType i_VehicleType, VehicleCreator.ePowerType i_EnergyType)
        {
            bool askAgain = true;
            float maxPower = GetMaxPower(i_VehicleType, i_EnergyType);
            float inputInFloat = 0;

            while(askAgain == true)
            {
                Console.WriteLine(@"How much power is your vehicle left? ");
                string inputFromUser = Console.ReadLine();

                if(float.TryParse(inputFromUser, out inputInFloat))
                {
                    if(inputInFloat <= maxPower)
                    {
                        askAgain = false;
                    }

                    else
                    {
                        Console.WriteLine("The value you entered is too big please try again");
                    }
                }

                else
                {
                    Console.WriteLine("Wrong input please try again");
                }
            }

            return inputInFloat;
        }

        public static float GetAirPressure(VehicleCreator.eVehicleType i_VehicleType, VehicleCreator.ePowerType i_EnergyType)
        {
            bool askAgain = true;
            float maxAir = GetMaxAir(i_VehicleType, i_EnergyType);
            float inputInFloat = 0;

            while(askAgain == true)
            {
                Console.WriteLine("Please enter current air pressure");
                string inputFromUser = Console.ReadLine();

                if(float.TryParse(inputFromUser, out inputInFloat))
                {
                    if (inputInFloat <= maxAir)
                    {
                        askAgain = false;
                    }

                    else
                    {
                        Console.WriteLine("The value you entered is too big please try again");
                    }
                }

                else
                {
                    Console.WriteLine("Wrong input please try again");
                }
            }

            return inputInFloat;
        }

        public static float GetMaxPower(VehicleCreator.eVehicleType i_VehicleType, VehicleCreator.ePowerType i_EnergyType)
        {
            float maxPower = 0;
            switch(i_VehicleType)
            {
                case VehicleCreator.eVehicleType.Car:
                    if(i_EnergyType == VehicleCreator.ePowerType.Gas)
                    {
                        maxPower = 50f;
                    }
                    else
                    {
                        maxPower = 1.6f;
                    }
                    break;
                case VehicleCreator.eVehicleType.Motorcycle:
                    if(i_EnergyType == VehicleCreator.ePowerType.Gas)
                    {
                        maxPower = 5.5f;
                    }
                    else
                    {
                        maxPower = 4.8f;
                    }

                    break;
                case VehicleCreator.eVehicleType.Truck:
                    maxPower = 105f;
                    break;
            }

            return maxPower;
        }

        public static float GetMaxAir(VehicleCreator.eVehicleType i_VehicleType, VehicleCreator.ePowerType i_EnergyType)
        {
            float maxAir = 0;
            switch(i_VehicleType)
            {
                case VehicleCreator.eVehicleType.Car:
                    maxAir = 32f;
                    break;
                case VehicleCreator.eVehicleType.Motorcycle:
                    maxAir = 28f;

                    break;
                case VehicleCreator.eVehicleType.Truck:
                    maxAir = 30f;
                    break;
            }

            return maxAir;
        }

        public static void ChangeVehicleStatus(Garage i_Garage)
        {
            bool askAgain = false;

            while(askAgain == false)
            {
                try
                {
                    Console.WriteLine("Please enter license number");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);
                    Console.WriteLine(
                        @"Please enter the new vehicle status:
1 - Repair
2 - Fixed
3 - Paid");
                    string newStatus = Console.ReadLine();
                    PrintLine();
                    CheckValidityStatus(newStatus);
                    if(i_Garage.IsVehicleExistByLicense(licenseNumber))
                    {
                        i_Garage.ChangeStatusOfVehicle(
                            licenseNumber,
                            (VehicleInformation.eVehicleCondition)int.Parse(newStatus));
                        Console.WriteLine("The status has changed successfully");
                    }
                    else
                    {
                        PrintTheLicenseDoseNotExist();
                    }

                    askAgain = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

        public static void CheckValidityLicenseNumber(string i_LicenseNumber)
        {
            if(!(i_LicenseNumber.All(char.IsNumber)))
            {
                throw new FormatException("Invalid format of license number");
            }
        }

        public static void CheckValidityStatus(string i_NewStatus)
        {
            if(i_NewStatus != "1" && i_NewStatus != "2" && i_NewStatus != "3")
            {
                throw new FormatException("Invalid status of vehicle");
            }
        }

        public static void AddAirToWheels(Garage i_Garage)
        {
            bool askAgain = false;
            while(askAgain == false)
            {
                try
                {
                    Console.WriteLine("Please enter license number");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);
                    if(i_Garage.IsVehicleExistByLicense(licenseNumber))
                    {
                        i_Garage.AddMaxAirToWheelOfTheVehicle(licenseNumber);
                        Console.WriteLine($@"The Wheels are full till the end: {i_Garage.VehiclesToFix[licenseNumber].VehicleOfOwner.Wheels[0].CurrentAirPressure}");
                       
                    }
                    else
                    {
                        PrintTheLicenseDoseNotExist();
                    }

                    askAgain = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

        public static void PrintTheLicenseDoseNotExist()
        {
            Console.WriteLine("Sorry, the license does not exist. You are returned to the main menu");
        }

        public static void ShowAllLicenseNumbers(Garage i_Garage)
        {
            Console.WriteLine(
                @"Do you want to filter the license numbers by the status of condition?
1 - for yes
2 - for no");
            string isFilter = Console.ReadLine();
            if(isFilter == "1")
            {
                ShowAllLicenseNumbersFilter(i_Garage);
            }
            else
            {
                ShowAllLicenseNumbersByRealOrder(i_Garage);
            }
        }

        public static void ShowAllLicenseNumbersFilter(Garage i_Garage)
        {
            int howManyCars = 0;
            Console.WriteLine(
                @"Please enter the filter for show license numbers:");
            int filterType = getEnumFromUser(typeof(VehicleInformation.eVehicleCondition));
            Console.WriteLine();

            foreach(KeyValuePair<string, VehicleInformation> entry in i_Garage.VehiclesToFix)
            {
                if((VehicleInformation.eVehicleCondition)filterType == entry.Value.VehicleCondition)
                {
                    Console.WriteLine(entry.Key);
                    howManyCars++;
                }
            }

            if(howManyCars == 0)
            {
                Console.WriteLine("There are no vehicles by this filter in the garage");
            }
        }

        public static void ShowAllLicenseNumbersByRealOrder(Garage i_Garage)
        {
            if(i_Garage.VehiclesToFix.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage currently, please try later");
            }
            else
            {
                foreach (KeyValuePair<string, VehicleInformation> entry in i_Garage.VehiclesToFix)
                {
                    Console.WriteLine(entry.Key);
                }
            }
        }

        public static void PrintCarByLicenseNumber(Garage i_Garage)
        {
            bool askAgain = false;
            while(askAgain == false)
            {
                try
                {
                    Console.WriteLine("Please enter license number");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);

                    if(i_Garage.IsVehicleExistByLicense(licenseNumber))
                    {
                        string informationOfVehicle = i_Garage.PrintSpecificVehicle(licenseNumber);
                        Console.WriteLine(informationOfVehicle);
                    }
                    else
                    {
                        PrintTheLicenseDoseNotExist();
                    }

                    askAgain = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

        public static void AddGasToVehicle(Garage i_Garage)
        {
            bool askAgain = false;
            while(askAgain == false)
            {
                try
                {
                    Console.WriteLine("Please enter license number");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);
                   
                    if (i_Garage.IsVehicleExistByLicense(licenseNumber))
                    {
                        i_Garage.CanCarBeFueled(licenseNumber);
                        Console.WriteLine("Please enter the liter of gas to add:");
                        string amountOfGas = Console.ReadLine();
                        Console.WriteLine(
                            @"Please enter the type of gas to add:");
                        int typeOfGas = getEnumFromUser(typeof(Gas.eGasType));

                        i_Garage.AddGas(licenseNumber, (Gas.eGasType)typeOfGas, float.Parse(amountOfGas));
                        Console.WriteLine($@"The vehicle has been fueled till: { i_Garage.VehiclesToFix[licenseNumber].VehicleOfOwner.PowerSource.CurrentAmountOfPower}");
                       
                    }
                    else
                    {
                            PrintTheLicenseDoseNotExist();
                    }
                    askAgain = true;
                }


                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }

            }
        }

        public static void AddElectricityToCar(Garage i_Garage)
        {
            bool askAgain = false;
            while(askAgain == false)
            {
                try
                {
                    Console.WriteLine("Please enter license number");
                    string licenseNumber = Console.ReadLine();
                    CheckValidityLicenseNumber(licenseNumber);

                    if(i_Garage.IsVehicleExistByLicense(licenseNumber))
                    {
                        i_Garage.CanCarBeCharged(licenseNumber);
                        Console.WriteLine("Please enter the amount of electricity to add:");
                        string amountOfElectricity = Console.ReadLine();

                        i_Garage.AddElectricity(licenseNumber, float.Parse(amountOfElectricity));
                        Console.WriteLine($@"The vehicle has been charged till: { i_Garage.VehiclesToFix[licenseNumber].VehicleOfOwner.PowerSource.CurrentAmountOfPower}");

                    }
                    else
                    {
                        PrintTheLicenseDoseNotExist();
                    }

                    askAgain = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

       
        private static void getDynamicParametersDataFromUser(Dictionary<string, Type> i_VehicleDynamicTypes, Dictionary<string, object> io_VehicleDynamicObjects)
        {
            foreach (string currentParam in i_VehicleDynamicTypes.Keys)
            {
                if (i_VehicleDynamicTypes[currentParam] == typeof(bool))
                {
                    getBoolParameter(currentParam, io_VehicleDynamicObjects);
                }
                else if (i_VehicleDynamicTypes[currentParam] == typeof(int))
                {
                    getIntParameter(currentParam, io_VehicleDynamicObjects);
                }
                else if (i_VehicleDynamicTypes[currentParam] == typeof(float))
                {
                    getFloatParameter(currentParam, io_VehicleDynamicObjects);
                }
                else if (i_VehicleDynamicTypes[currentParam].IsEnum)
                {
                    getEnumParameter(currentParam, io_VehicleDynamicObjects, i_VehicleDynamicTypes[currentParam]);
                }
            }
        }

        
        private static void getIntParameter(string i_CurrentParam, Dictionary<string, object> io_DynamicOjects)
        {
            Console.WriteLine($@"Please enter {i_CurrentParam} as a positive number");
            io_DynamicOjects.Add(i_CurrentParam, getIntInput());
        }

         
        private static void getFloatParameter(string i_CurrentParam, Dictionary<string, object> io_DynamicObjects)
        {
            Console.WriteLine($@"Please enter {i_CurrentParam} as a positive number");
            io_DynamicObjects.Add(i_CurrentParam, getFloatInput());
        }

        private static int getEnumFromUser(Type i_Enum)
        {
            string indexOfEnum;
            Array valuesOfEnum = Enum.GetValues(i_Enum);
            int amountOfEnum = valuesOfEnum.Length;
            bool isParseNumber, isValidEnum = false;
            int indexOfEnumValue = 0;


            while(isValidEnum == false)
            {
                int currentValueIndex = 1;
                Console.WriteLine("Choose one of the following: ");
                    foreach (object enumValue in valuesOfEnum)
                    {
                        Console.WriteLine($@"{currentValueIndex}-{enumValue}");
                        currentValueIndex++;
                    }
                    
                indexOfEnum = Console.ReadLine();
                isParseNumber = int.TryParse(indexOfEnum, out indexOfEnumValue);
                if (isParseNumber && indexOfEnumValue >= 1 && indexOfEnumValue <= amountOfEnum)
                {
                    isValidEnum = true;
                }
                else
                {
                    Console.WriteLine("choice doesn't exist , please try again");
                }
            }

            return indexOfEnumValue;
        }

        
        private static void getEnumParameter(string i_EnumParam, Dictionary<string, object> io_DynamicParams, Type i_EnumType)
        {
            Console.WriteLine($@"Please enter { i_EnumParam}.");
            io_DynamicParams.Add(i_EnumParam, getEnumFromUser(i_EnumType));
        }

       
        private static void getBoolParameter(string i_BoolParam, Dictionary<string, object> io_DynamicParams)
        {
            string userInput;
            int userInputAsInt;
            bool isValidBool = false;
            Console.WriteLine($@"Choose {i_BoolParam}? Press 1 for 'True' ,press 2 for 'False'.");

            while(isValidBool == false)
            {
                userInput = Console.ReadLine();
                bool success = int.TryParse(userInput, out userInputAsInt);
                if(success && (userInputAsInt == 1 || userInputAsInt == 2))
                {
                    bool boolValue = (userInputAsInt == 1);
                    io_DynamicParams.Add(i_BoolParam, boolValue);
                    isValidBool = true;
                }
                else
                {
                    Console.WriteLine("Not a boolean option , please try again ");
                }
            }
        }

        public static void PrintLine()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine(Environment.NewLine);
        }

        private static float getFloatInput()
        {
            string inputOfuser;
            bool parsingWorked = false;
            float inputOfUserFloat = 0;

            while(parsingWorked == false)
            {
                
                inputOfuser = Console.ReadLine();
                parsingWorked  = float.TryParse(inputOfuser, out inputOfUserFloat);

                if(parsingWorked == true)
                {
                    if(inputOfUserFloat < 0)
                    {
                        Console.WriteLine("Please only enter positive numbers, try again");
                        parsingWorked = false;
                    }
                }
                else
                {

                    Console.WriteLine("Wrong format, please try again");

                }
            }
            return inputOfUserFloat;
        }

        private static int getIntInput()
        {
            string inputOfuser;
            bool parsingWorked = false;
            int inputOfUserInt = 0;

            while(parsingWorked == false)
            {

                inputOfuser = Console.ReadLine();
                parsingWorked = int.TryParse(inputOfuser, out inputOfUserInt);

                if(parsingWorked == true)
                {
                    if(inputOfUserInt < 0)
                    {
                        Console.WriteLine("Please only enter positive numbers, try again");
                        parsingWorked = false;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format, please try again");
                }
            }
            return inputOfUserInt;
        }
    }
}
