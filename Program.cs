using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace ConsoleApp28
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while(!exit)
            {
                DocumentOfOder[] arrayOfOrders = Orders_Check("EQUALIZER.txt");
                int listsize = arrayOfOrders.Length;
                List<DocumentOfOder> MainList = new List<DocumentOfOder>(listsize);
                MainList = arrayOfOrders.ToList();
                Console.WriteLine("WELCOME TO BENNIES SIKANWE SERVICE CENTRE");
                Console.WriteLine("YOU MAY PLACE AN ORDER OR WORK WITH THE FOLLOWING");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотреть все данные");
                Console.WriteLine("2. Добавить новый заказ");
                Console.WriteLine("3. Удалить заказ");
                Console.WriteLine("4. Поиск данных заказов");
                Console.WriteLine("5. Проверка Состояния");
                Console.WriteLine("6. Проверка общего дохода За короткий период");
                Console.WriteLine("7. Проверьте исправленные данные за длительный период в соответствии с данными, которые у вас есть в файла");
                Console.WriteLine("8. Выход");
                Console.WriteLine("Введите номер по вашему выбору");
                string Choice = Console.ReadLine();
                switch (Choice)
                {
                    case "1":
                        {
                            ShowInfo(arrayOfOrders);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "2":
                        {
                            AddinAnotherOrder("EQUALIZER.txt");
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter the Index of The Client You want To Remove Data");
                            Console.Write("Index: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            RemoveAnOrder(ref arrayOfOrders, index);
                            WritingToFile("EQUALIZER.txt", arrayOfOrders);
                            ShowInfo(arrayOfOrders);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "4":
                        {
                            ClientSearch(arrayOfOrders, "EQUALIZER.txt");
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "5":
                        {
                            Order_Status(MainList, listsize);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "6":
                        {
                            Company_RevenueFromAnIndividual(MainList);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "7":
                        {
                            Company_RevenueFromAnIndividual2(MainList);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "8":
                        {
                            Console.Clear();
                            Console.WriteLine("успешно выход из программы");
                            exit = true;
                        }
                        break;
                }
           
            }
        }
        #region - Periodic Reveue Addition
        static bool Company_RevenueFromAnIndividual2(List<DocumentOfOder> Revenue_Check)
        {
            Console.Clear();
            Console.WriteLine("Enter Company dates to check How much Revenue the Service Centre genearted on a Particular Period Of Days");
            Console.WriteLine("Hey!!There Here Enter the Date when the Product was Submitted");
            Console.Write("Press 1 To Start Check: ");
            int date_Verification = int.Parse(Console.ReadLine());
            double ValueFromSddedDates = 0;
            int x =1;
            double Total_Revenue = 0;
            for (int j = date_Verification; j < date_Verification+x;)
            {
                Console.WriteLine("Hey!!There Here Enter the Date when the Product was Submitted");
                Console.Write("Date Entry Format(dd-MM-yyyy):");
                string Acceptance = Console.ReadLine();
                Console.WriteLine("Hey!!There,Here Enter the Date when the Product was issued Out");
                Console.Write("Date Entry Format(dd-MM-yyyy):");
                string Issueing = Console.ReadLine();
                var CheckForRevenue1 = Revenue_Check.FindAll(date_Entry => date_Entry.DateOFAcceptance == Acceptance);
                var CheckForRevenue2 = Revenue_Check.FindAll(date_Entry => date_Entry.DateOfIssue == Issueing);
                foreach (DocumentOfOder dateIn in CheckForRevenue1)
                {
                    foreach (DocumentOfOder dateOut in CheckForRevenue2)
                    {
                        if (CheckForRevenue1.Count > 0 && CheckForRevenue2.Count > 0)
                        {
                            Console.WriteLine("Are sure  you dont want to entre another preiod(Dates)");
                            Console.WriteLine("Choose An Option and enter either 1 or 2: ");
                            Console.WriteLine("1. Yess I want to Enter another period");
                            Console.WriteLine("2. Noo i found what i was looking");
                            string DecisionMadeByUser = Console.ReadLine();
                            switch (DecisionMadeByUser)
                            {
                                case "1":
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Okay You May add");
                                        j++;
                                        x++;
                                        Total_Revenue = Total_Revenue + dateIn.Cost;
                                    }
                                    break;
                                case "2":
                                    {
                                        Console.WriteLine("Here is Your Revenue over the Dates you Enter");
                                        j += 2; Total_Revenue = Total_Revenue + dateIn.Cost;
                                    }
                                    break;
                            }
                        }
                    }
                }
                
            }
            Console.Clear();
            Console.WriteLine("Общий доход за указанные вами даты: " + Total_Revenue);
            
            return true;
        }

        #endregion
        #region - Company_RevenueFromAnIndividual
        static bool Company_RevenueFromAnIndividual(List<DocumentOfOder> Revenue_Check)
        {
            Console.Clear();
            Console.WriteLine("Enter Company dates to check How match Revenue the Service Centre genearted on a Particular day.");
            Console.WriteLine("Hey!!There,Here Enter the Date when the Product was Submitted.");
            Console.Write("Date Entry Format(dd-MM-yyyy):");
            string Acceptance = Console.ReadLine();
            Console.WriteLine("Hey!!There,Here Enter the Date when the Product was issued Out.");
            Console.Write("Date Entry Format(dd-MM-yyyy):");
            string Issueing = Console.ReadLine();   
            var CheckForRevenue1 = Revenue_Check.FindAll(date_Entry => date_Entry.DateOFAcceptance == Acceptance);
            var CheckForRevenue2 = Revenue_Check.FindAll(date_Entry => date_Entry.DateOfIssue == Issueing);
            foreach(DocumentOfOder dateIn in CheckForRevenue1)
            {
                foreach (DocumentOfOder dateOut in CheckForRevenue2)
                {
                    if(CheckForRevenue1.Count > 0 && CheckForRevenue2.Count > 0)
                    {
                        Console.WriteLine("Revenue Check Result");
                        Console.WriteLine($"The Revenue Generated from Date {dateIn.DateOFAcceptance} To {dateIn.DateOfIssue} is ${dateIn.Cost} " +
                            $"from Client {dateIn.CLient_Name}");
                    }
                    else
                    {
                        Console.WriteLine("Please Your Entered the WRONG date");
                        Console.WriteLine($"The Dates {Acceptance} and {Issueing} are not Correct");
                    }
                }
            }
            return true;
        }
        #endregion

        #region - OrderStatus_Complete/Incomplete
        static bool Order_Status( List<DocumentOfOder> StatusCompletion,int size)
        {
            Console.Clear();
            Console.WriteLine("Check For Incomplte Product Status");
            string press = "Incomplete";
            var matchingstatus = StatusCompletion.FindAll(elements => elements.Status == press);
            foreach (DocumentOfOder item in matchingstatus)
            {
                if(matchingstatus.Count> 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Name:{item.CLient_Name}");
                    Console.WriteLine($"ProductName:{item.ProductName}  ");
                    Console.WriteLine($"Model:{item.Model_Or_Brand}"); Console.WriteLine($"Status:{item.Status} ");
                    Console.WriteLine($"Your Product Had to be Done On {item.DateOfIssue} ");
                    Console.WriteLine("but we finish working on it Two days from now");
                }
            }
            
            return true;
        }
        #endregion

        #region-Client Search
        static bool ClientSearch(DocumentOfOder[] Search, string documentation)
        {
            DocumentOfOder []matching ;
            Console.WriteLine("Hey Press the Index of the client detials you want to find out");
            string printNumber = Console.ReadLine();
            //Orders_Check(documentation);
                    matching =Array.FindAll(Search,element => element.Personal_Contact == printNumber);
                    foreach(var info in matching)
                    {
                        using(StreamReader reading = new StreamReader(documentation))
                        {
                           while(!reading.EndOfStream)
                            {
                                string line = reading.ReadLine();
                                if (line.Contains(info.CLient_Name) && line.Contains(info.Personal_Contact) && line.Contains(info.ProductName))
                                {
                                    Console.WriteLine(line);
                                }
                            }
                        }
                    }
                    return true;
        }
        #endregion

        #region- Reading From File
        static DocumentOfOder[] Orders_Check (string path)
        {
            string[] FileData = File.ReadAllLines(path);
            int filelines = FileData.Length;
            DocumentOfOder[] ClientDetials = new DocumentOfOder[filelines];
            
                
                DocumentOfOder data;
                for (int i = 0; i < filelines; i++)
                {
                    string[] DataIndex = FileData[i].Split(new char[] { ';' });
                    data.CLient_Name = DataIndex[0];
                    data.ProductName = DataIndex[1];
                    data.Model_Or_Brand= DataIndex[2];
                    data.Personal_Contact = DataIndex[3];
                    data.Cost = Convert.ToDouble(DataIndex[4]);
                    data.DateOFAcceptance = DataIndex[5];
                    data.Status = DataIndex[6];
                    data.DateOfIssue = DataIndex[7];
                    ClientDetials[i] = data;
                }
            return ClientDetials;
        }
        #endregion

        #region - Display
        static void ShowInfo(DocumentOfOder[] ClientDetials)
        {
            //Console.Clear();
            for (int i = 0; i < ClientDetials.Length; i++)
                {
                    Console.WriteLine(ClientDetials[i].CLient_Name+" " + ClientDetials[i].ProductName+" " + ClientDetials[i].Model_Or_Brand+" "+ 
                        ClientDetials[i].Personal_Contact+" " + "$"+ClientDetials[i].Cost+" " + ClientDetials[i].DateOFAcceptance+" " 
                        + ClientDetials[i].Status+" " + ClientDetials[i].DateOfIssue);
                }
        }
        #endregion

        #region - Adding Another Order
        static void AddinAnotherOrder(string Additional_Order)
        {
            Console.WriteLine("HERE ADD NEW CLIENT DATA AND HOW MANY CLIENTS DO YOU WANT TO ADD");
            Console.Write("NUMBER: ");
            int addedDataSize = Convert.ToInt32(Console.ReadLine());
            string[] addingArray= new string[addedDataSize];
            {
                for (int i = 0; i < addingArray.Length; i++)
                {
                    Console.Write("Client Name:");
                    string CLient_Name = Console.ReadLine();
                    Console.Write("Product Name: ");
                    string ProductName = Console.ReadLine();
                    Console.Write("Model/Brand: ");
                    string Model_Or_Brand = Console.ReadLine();
                    Console.Write("Personal_Contact: ");
                    string Personal_Contact = Console.ReadLine();
                    Console.Write("Cost :$");
                    string Cost = Console.ReadLine();
                    Console.Write("Date_Acceptance(dd-MM-yyyy): ");
                    string DateGiven = Console.ReadLine();
                    Console.WriteLine("Enter a Number From Below");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" ");
                    sb.Append("1.Complete");
                    Console.WriteLine(sb);
                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append(" ");
                    sb2.Append("2.Incomplete");
                    Console.WriteLine(sb2);
                    string Choose_Status = Console.ReadLine();
                    switch (Choose_Status)
                    {
                        case "1":
                            Console.Write("Status: Complete");
                            string status = "Complete" ;
                            Console.Write("Date_Isssued(dd-MM-yyyy):");
                            string DateGotten = Console.ReadLine();
                            using (StreamWriter Writing = new StreamWriter("EQUALIZER.txt", true))
                            {
                                Writing.WriteLine($"{CLient_Name};{ProductName};{Model_Or_Brand};" +
                                    $"{Personal_Contact};{Cost};{DateGiven};{status};{DateGotten}");
                            }
                            break;
                        case "2":
                            Console.Write("Staus: Incomplete");
                            string status2 = "Incomplete";
                            Console.Write("Date_Isssued(dd-MM-yyyy):");
                            string DateGotten2 = Console.ReadLine();
                            using (StreamWriter Writing = new StreamWriter("EQUALIZER.txt", true))
                            {
                                Writing.Write($"{CLient_Name};{ProductName};{Model_Or_Brand};" +
                                    $"{Personal_Contact};{Cost};{DateGiven};{status2};{DateGotten2}");
                            }
                            break;
                            
                    }
                    Console.WriteLine();
                }       
            }
        }
        #endregion

        #region - Remove An Order
        static void RemoveAnOrder(ref DocumentOfOder[] RemoveCertainIndex, int indexPosition ) 
        {
            
            for (int i = 1 + indexPosition; i < RemoveCertainIndex.Length; i++)
            {
                RemoveCertainIndex[i - 1] = RemoveCertainIndex[i];
            }
            Array.Resize(ref RemoveCertainIndex, RemoveCertainIndex.Length-1);
        }
        #endregion

        #region - Writing toFile

        static void WritingToFile(string File, DocumentOfOder[]TypeWriter)
        {
            using(StreamWriter PrintToFile = new StreamWriter(File, false)) 
            {
                for (int i = 0; i < TypeWriter.Length; i++)
                {
                    PrintToFile.WriteLine($"{TypeWriter[i].CLient_Name};{TypeWriter[i].ProductName};{TypeWriter[i].Model_Or_Brand};" +
                        $"{TypeWriter[i].Personal_Contact};{TypeWriter[i].Cost};{TypeWriter[i].DateOFAcceptance};" +
                        $"{TypeWriter[i].Status}.{TypeWriter[i].DateOfIssue}");
                }
            }
        }
        #endregion
        struct DocumentOfOder
        {
            public string CLient_Name;
            public string ProductName;
            public string Model_Or_Brand;
            public string Personal_Contact;
            public double Cost;
            public string DateOFAcceptance;
            public string Status;
            public string DateOfIssue;
        }
    }
}    

