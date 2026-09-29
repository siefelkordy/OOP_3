namespace OOP_3
{
    #region Part1: Theoritical Questions
    //Part1: Theoritical
    //Question1:
    //a) Method Overloading happens in the static polymorphism while Method Overriding happens in the dynamic polymorphism
    //Method overloading is to have multiple methods with the same name but different parameters number or types if you want to overload a method in the child class you have to use the new keyword
    //Method overriding is to have the parent class allow the child class to modify its method using virtual keyword and use override keyword in the child class to modify the method
    //In main,Compiler choose the method dependent on the refernce type in the method overloading
    //In main,Compiler choose the method dependent on the least dervied class type in the method overriding
    //b)
    //Question2:
    //a)
    //b)
    #endregion

    #region Part2: Practical Questions

    #region DeliveryAdress Class
    public struct DeliveryAddress
    {
        string City;
        string Street;
        int BuildingNumber;
        public DeliveryAddress(string city, string street, int buildingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }
        public DeliveryAddress(string street)
        {
            Street = street;
            City = "New York";
            BuildingNumber = 1;
        }
        public string GetFullAddress()
        {
            return $"{BuildingNumber} {Street} ,{City}";
        }


    }
    #endregion

    #region Shipment Class (Parent Class)
    //Shipment Class(Parent Class)
    public class Shipment
    {
        string trackingCode;
        string description;
        int weight;
        decimal deliveryFee;
        DeliveryAddress destination;


        //Properties
        //1.Tracking Code Property
        public string TrackingCode
        {
            get
            {
                return trackingCode;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    trackingCode = value;
                }
                else
                {
                    throw new ArgumentException("Tracking Code can't be null or empty");
                }
            }
        }
        //2.Description Property
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                else
                {
                    description = value;
                }
            }
        }
        //3.Weight Property
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Weight must be a positive number.");
                }
                weight = value;
            }

        }
        //4.Delivery Fee Property
        public decimal DeliveryFee
        {
            get
            {
                return deliveryFee;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Delivery fee must be a positive number.");
                }
                else
                {
                    deliveryFee = value;
                }
            }
        }
        //5.Destination Property
        public DeliveryAddress Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
            }
        }
        public virtual decimal EstimatedCost
        {
            get
            {
                return deliveryFee + (weight * 5);
            }
        }

        //////////////Constructors
        //1st Constructor
        public Shipment(string trackingCode)
        {
            this.trackingCode = trackingCode;
            description = "Unknown";
            weight = 1;
            deliveryFee = 50;
            destination = new DeliveryAddress("Nasr city", "Al Nahas", 15);
        }
        //2nd Constructor
        public Shipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee)
        {
            trackingCode = TrackingCode;
            description = Description;
            weight = Weight;
            deliveryFee = DeliveryFee;
        }
        //////////////Methods
        //Update DeliveryFee Method
        public void UpdateDeilveryFee(decimal newFee)
        {
            if (newFee > 0)
            {
                deliveryFee = newFee;
            }
            else
            {
                throw new ArgumentException("Delivery fee must be a positive number.");
            }
        }
        public void UpdateDeilveryFee(decimal newFee,int packingWeight)
        {
            if (newFee > 0)
            {
                deliveryFee = newFee + (packingWeight*5);
            }
            else
            {
                throw new ArgumentException("Delivery fee must be a positive number.");
            }
        }
        //Print Shipment Method
        public virtual string PrintShipmentDetails()
        {
            return $"Tracking Code: {TrackingCode}\nDescription: {Description}\nWeight: {Weight}kg\nDelivery Fee: {deliveryFee}\nEstimated Cost: {EstimatedCost}";
        }
    }
    #endregion

    #region Standard Shipment Class(Child Class)
    //Standard Shipment Class(Child Class)
    public class StandardShipment : Shipment
    {
        //Chaining Constructor
        public StandardShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
        }
        //Print Shipment Override method
        public override string PrintShipmentDetails()
        {
            return "Standard Shipment\n\n" + base.PrintShipmentDetails();
        }
    }
    #endregion

    #region Express Shipment Class(Child Class)
    //Express Shipment Class(Child Class)
    public class ExpressShipment : Shipment
    {
        decimal extrafee;
        //ExtraFee Property
        public decimal ExtraFee
        {
            get
            {
                return extrafee;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Extra fee must be greater than or equal to 0");
                else
                    extrafee = value;
            }
        }
        //Override Estimated cost property
        public override decimal EstimatedCost
        {
            get
            {
                return base.EstimatedCost + extrafee;
            }

        }
        //Print Shipment Override method
        public override string PrintShipmentDetails()
        {
            return "Express Shipment\n\n" + base.PrintShipmentDetails() + $"\nExtra Fee: {extrafee}";
        }
        public ExpressShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee, decimal extrafee) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
            this.ExtraFee = extrafee;
        }
    }
    #endregion

    #region International Shipment Class(parent class)
    //International Shipment Class(parent class)
    public class InternationalShipment : Shipment
    {
        string destinationcountry;
        decimal customsfee;
        //Destination Country property
        public string DestinationCountry
        {
            get
            {
                return destinationcountry;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                else
                {
                    destinationcountry = value;
                }


            }
        }
        //Customs Fee property
        public decimal CustomsFee
        {
            get
            {
                return customsfee;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Extra fee must be greater than or equal to 0");
                else
                    customsfee = value;
            }
        }
        //Override Estimated cost property
        public override decimal EstimatedCost
        {
            get
            {
                return base.EstimatedCost + customsfee ;
            }

        }
        //Print Shipment Override method
        public override string PrintShipmentDetails()
        {
            return "International Shipment\n\n" + base.PrintShipmentDetails() + $"\nCustoms Fee: {customsfee}" + $"\nDestination: {destinationcountry}";
        }
        //Constructor chaining
        public InternationalShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee, decimal customsfee, string destinationcountry) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
            this.CustomsFee = customsfee;
            this.DestinationCountry = destinationcountry;
        }
    }
    #endregion

    #region Delivery Center Class
    //Delivery Center Class
    public class DeliveryCenter
    {
        public string CenterName;
        public string AssignedDriver;
        Shipment[] shipment;
        public DeliveryCenter()
        {
            shipment = new Shipment[20];
        }
        //Integer Indexer
        public Shipment this[int index]
        {
            get
            {
                if (index >= 0 && index < shipment.Length)
                {
                    return shipment[index];
                }
                return default;
            }
            set
            {
                if (index >= 0 && index < shipment.Length)
                {
                    shipment[index] = value;
                }
            }
        }
        //String Indexer
        public Shipment this[string trackingcode]
        {
            get
            {
                for (int i = 0; i < shipment.Length; i++)
                {
                    if (shipment[i] != null &&
                        shipment[i].TrackingCode == trackingcode)
                    {
                        return shipment[i];
                    }
                }

                return null;
            }
        }
        //AddShipment Method
        public bool AddShipment(Shipment newshipment)
        {
            for (int i = 0; i < shipment.Length; i++)
            {
                if (shipment[i] == null)
                {
                    shipment[i] = newshipment;
                    return true;
                }
            }

            return false;
        }
        //RemoveShipment Method
        public bool RemoveShipment(string trackingcode)
        {
            for (int i = 0; i < shipment.Length; i++)
            {
                if (shipment[i].TrackingCode != null && shipment[i].TrackingCode == trackingcode)
                {
                    shipment[i] = null;
                    return true;
                }
            }
            return false;
        }
        //Print all shipments method
        public void PrintAllShipments()
        {
            foreach(Shipment s in shipment)
            {
                if (s != null)
                {
                    Console.WriteLine(s.PrintShipmentDetails());
                }
            }
        }
    }
    #endregion

    #region Delivery Helper Class
    public static class DeliveryHelper 
    { 
    public static void PrintShipmentDetails(Shipment shipment)
        {
            if(shipment != null)
            Console.WriteLine(shipment.PrintShipmentDetails());
        }
    }
    #endregion

    #region Driver Class
    public class Driver
    {
        public string name 
        { get; 
            set
            {
                if (value is null || value is "")
                    throw new ArgumentException("Please enter a valid driver's name");
            }    
        }

        public Driver (string name)
        {
            this.name = name;
        }
    
    }
    #endregion


    #region Main
    internal class Program
    {
        static void Main(string[] args)
        {
            //a.Create a Driver
            Console.WriteLine("Enter Driver Name:");
            string driverName = Console.ReadLine();

            Driver driver = new Driver(driverName);
            // b. Create a DeliveryCenter
            Console.WriteLine("Enter Delivery Center Name:");
            string centerName = Console.ReadLine();

            DeliveryCenter center = new DeliveryCenter();
            center.CenterName = centerName;

            //c.Assign driver to delivery center
            center.AssignedDriver = driver.name;
            // d. Create Standard Shipment
            Console.WriteLine("\n--- Standard Shipment ---");

            Console.WriteLine("Tracking Code:");
            string standardTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string standardDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int standardWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal standardDeliveryFee = decimal.Parse(Console.ReadLine());

            StandardShipment standardShipment =
                new StandardShipment(
                    standardTrackingCode,
                    standardDescription,
                    standardWeight,
                    standardDeliveryFee
                );


            // e. Create Express Shipment
            Console.WriteLine("\n--- Express Shipment ---");

            Console.WriteLine("Tracking Code:");
            string expressTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string expressDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int expressWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal expressDeliveryFee = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Extra Fee:");
            decimal extraFee = decimal.Parse(Console.ReadLine());

            ExpressShipment expressShipment =
                new ExpressShipment(
                    expressTrackingCode,
                    expressDescription,
                    expressWeight,
                    expressDeliveryFee,
                    extraFee
                );


            // f. Create International Shipment
            Console.WriteLine("\n--- International Shipment ---");

            Console.WriteLine("Tracking Code:");
            string internationalTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string internationalDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int internationalWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal internationalDeliveryFee = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Destination Country:");
            string destinationCountry = Console.ReadLine();

            Console.WriteLine("Customs Fee:");
            decimal customsFee = decimal.Parse(Console.ReadLine());

            InternationalShipment internationalShipment =
                new InternationalShipment(
                    internationalTrackingCode,
                    internationalDescription,
                    internationalWeight,
                    internationalDeliveryFee,
                    customsFee,
                    destinationCountry

                );


            // g. Add shipments to Delivery Center
            center.AddShipment(standardShipment);
            center.AddShipment(expressShipment);
            center.AddShipment(internationalShipment);


            // h & i. Print all shipments
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Delivery Center: {center.CenterName}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"\nDriver Name: {driver.name}");
            Console.WriteLine("------------------------------------");
            //center.PrintAllShipments();
            for (int i = 0; i < 20; i++)
            {
                if (center[i] != null)
                {
                    DeliveryHelper.PrintShipmentDetails(center[i]);
                    Console.WriteLine("------------------------------------");
                }
            }
            Console.WriteLine("Printing Using DeliveryHelper...\nStandard Shipment Printed Successfully.\nExpress Shipment Printed Successfully.\nInternational Shipment Printed Successfully.\n");
            Console.WriteLine("------------------------------------");

            //j. demonstrate both versions of update weight
            int updatedWeight = 5;
            standardShipment.UpdateDeilveryFee(20.5m);
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Original Weight : {expressShipment.Weight} KG");
            expressShipment.Weight = updatedWeight;
            expressShipment.UpdateDeilveryFee(50.0m, updatedWeight);
            Console.WriteLine($"Updated Weight : {expressShipment.Weight} KG");
            Console.WriteLine($"Updated Weight after Packing : {expressShipment.Weight + 0.5} KG");

            //k. Build a Shipment[] holding mixed types and print all of them in a loop.
            Shipment[] mixedShipments = new Shipment[]
        {
            new StandardShipment("STD-101", "Textbooks", 3, 45.0m),
            new ExpressShipment("EXP-202", "Smartphone", 1, 80.0m, 25.0m),
            new InternationalShipment("INT-303", "Machine Parts", 12, 200.0m, 75.0m, "Canada")
        };
            Console.WriteLine("========== MIXED SHIPMENTS ==========");
            foreach (Shipment s in mixedShipments)
            {
                if (s != null)
                {
                    Console.WriteLine(s.PrintShipmentDetails());
                    Console.WriteLine("------------------------------------------");
                }
            }

            //// 7. Search using the tracking-code indexer
            //Console.WriteLine("\nEnter Tracking Code to Search:");
            //string searchCode = Console.ReadLine();

            //Shipment foundShipment = center[searchCode];

            //if (foundShipment != null)
            //{
            //    Console.WriteLine("\nShipment Found:");
            //    Console.WriteLine(foundShipment.PrintShipmentDetails());
            //}
            //else
            //{
            //    Console.WriteLine("No Shipment Found!!");
            //}


            //// 8. Remove shipment
            //Console.WriteLine("\nEnter Tracking Code to Remove:");
            //string removeCode = Console.ReadLine();

            //bool removed = center.RemoveShipment(removeCode);

            //if (removed)
            //{
            //    Console.WriteLine("Shipment Removed Successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("Shipment Not Found.");
            //}


            //// 9. Print remaining shipments
            //Console.WriteLine("\n========== REMAINING SHIPMENTS ==========");
            //center.PrintAllShipments();
        }
    }
    #endregion

    #endregion


}


