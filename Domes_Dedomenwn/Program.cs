using System;
using System.IO;
using System.Collections.Generic;

namespace Domes_Dedomenwn
{
    public class Reservation
    {
        public string name { get; set; }
        public DateTime checkinDate { get; set; }
        public int stayDurationDays { get; set; }
        public Reservation(string Name, DateTime CheckinDate, int StayDurationDays)
        {
            name = Name;
            checkinDate = CheckinDate;
            stayDurationDays = StayDurationDays;
        }
    }

    public class Hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stars { get; set; }
        public int numberOfRooms { get; set; }
        private List<Reservation> _reservations = new List<Reservation>();
        public List<Reservation> reservations
        {
            get { return _reservations; }
        }
        public Hotel(int Id, string Name, int Stars, int NumberOfRooms)
        {
            id = Id;
            name = Name;
            stars = Stars;
            numberOfRooms = NumberOfRooms;
        }
    }

    public class Running
    {
        private static int GetChoice()
        {
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("Select an option by typing the number it represents..");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1.Load Hotels and Reservations from file\n2.Save Hotels and Reservations to file\n3.Add a Hotel");
                Console.WriteLine("4.Search and Display a Hotel by id\n5.Display Reservations by surname search\n6.Exit");
                Console.WriteLine("-------------------------------------------");
                string input = Console.ReadLine();
                int choice;
                if(int.TryParse(input, out choice)) {return choice;}
                else { Console.WriteLine(); Console.WriteLine("Please enter a number!");}
            }
        } //END GetChoice()

        private static int GetSearchChoice()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Select a search option by typing the number it represents..");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1.Linear Search\n2.Binary Search\n3.Interpolation Search\n4.AVL-tree Search");
                Console.WriteLine("-----------------------");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice)) { return choice; }
                else { Console.WriteLine(); Console.WriteLine("Please enter a number!"); }
            }
        } //END GetSearchChoice()

        private static int GetSearchChoice_2()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Select a search option by typing the number it represents..");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1.Linear Search\n2.Digital Tries Search");
                Console.WriteLine("-----------------------");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice)) { return choice; }
                else { Console.WriteLine(); Console.WriteLine("Please enter a number!"); }
            }
        } //END GetSearchChoice_2()

        private static void BubbleSort(Hotel []hotel)
        {
            Hotel temp;
            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                for (int i = 0; i < hotel.Length - 1; i++)
                {
                    if (hotel[i].id > hotel[i + 1].id)
                    {
                        temp = hotel[i];
                        hotel[i] = hotel[i + 1];
                        hotel[i + 1] = temp;
                        swapped = true;
                    }
                }
            }
        } //END BubbleSort()

        static void Main(string[] args)
        {
            //Arxikopoiisi timwn
            int choice = 0;
            int allow_2 = 0;
            int h_number = 0;
            int saveflag = 0;
            int []r_num;
            r_num = new int[h_number];
            string answer = "n";
            string answer_2 = "n";
            string answer_3 = "y";
            Hotel[] hotel = new Hotel[0];
            string filename = "data.csv";
            Console.Write("File name: ");
            filename = Console.ReadLine();
            if (filename == "") filename = "data.csv";
            Console.WriteLine("*Hotel Data Management*\nWelcome!");
            while(choice != 6)
            {
                choice = GetChoice();
                //Periorismoi
                while (choice < 1 || choice > 6)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option!\nPlease enter a number between 1-6");
                    choice = GetChoice();
                }
                //LOAD FILE
                if (choice == 1)
                {
                    string line;
                    StreamReader file = new StreamReader(filename);
                    //Diavazei ti prwti grammi pou periexei ton arithmo twn hotel
                    line = file.ReadLine();
                    //Afairesi twn ';'
                    string substr = line.Substring(0, line.IndexOf(';'));
                    //Metatropi String se int
                    h_number = Int32.Parse(substr);
                    r_num = new int[h_number];
                    hotel = new Hotel[h_number];
                    int a = 0;
                    //Oso uparxoun stoixeia sto file, diladi den ftasame se keni seira
                    while ((line = file.ReadLine()) != null)
                    {
                        //Arxikopoiisi timwn
                        int counter = 0;
                        int Id = 0;
                        string Name = "";
                        int Stars = 0;
                        int NumberOfRooms = 0;
                        string r_Name = "";
                        int StayDurationDays = 0;
                        DateTime CheckinDate = DateTime.Parse("01/01/2000");
                        string data = "";
                        int allow = 0;
                        int rep = 0;
                        int len = line.Length;
                        for (int i = 0; i < len; i++)
                        {
                            //Diaxwrismos leksewn
                            if (line[i] == ';')
                            {
                                data = line.Substring(rep, i - rep);
                                rep = i + 1;
                                allow = 1;
                            }
                            //Periptwsi --> teleutaia metavliti den exei ';' sto telos
                            else if (i == len - 1)
                            {
                                data = line.Substring(rep, i - rep + 1);
                                allow = 1;
                            }
                            //Molis paroume olokliri leksi tin apothikeuoume stin katallili metavliti
                            if (allow == 1)
                            {
                                //Apothikeusi stoixeiwn Hotel
                                if (counter == 0) { Id = Int32.Parse(data); }
                                else if (counter == 1) { Name = data; }
                                else if (counter == 2) { Stars = Int32.Parse(data); }
                                else if (counter == 3)
                                {
                                    NumberOfRooms = Int32.Parse(data);
                                    hotel[a] = new Hotel(Id, Name, Stars, NumberOfRooms);
                                }
                                //Apothikeusi stoixeiwn Reservation
                                else if (counter == 4) { r_Name = data; }
                                else if (counter == 5) { CheckinDate = DateTime.Parse(data); }
                                else if (counter == 6)
                                {
                                    StayDurationDays = Int32.Parse(data);
                                    hotel[a].reservations.Add(new Reservation(r_Name, CheckinDate, StayDurationDays));
                                    r_num[a]++;
                                    counter = 3;
                                    r_Name = "";
                                    StayDurationDays = 0;
                                    CheckinDate = DateTime.Parse("01/01/2000");
                                }
                                counter++;
                                data = "";
                            }
                            allow = 0;
                        }
                        a++;
                    }
                    //Telos arxeiou
                    file.Close();
                    allow_2 = 1;
                    Console.WriteLine("Arithmos hotel: {0}\n", h_number);
                }
                //Periorismos --> Eisagwgi stis epomenes epiloges mono afou kanoume load to file
                if (allow_2 == 1)
                {
                    //Apothikeusi tou file
                    if (choice == 2)
                    {
                        StreamWriter save = new StreamWriter(filename);
                        //Edw apothikeueis ton arithmo twn hotel giati den perases sto pinaka
                        string num = h_number.ToString();
                        save.WriteLine("{0};;;;", num);
                        //Edw apothikeueis ta hotel...
                        for (int i = 0; i < h_number; i++)
                        {
                            string w_Id = hotel[i].id.ToString();
                            string w_Name = hotel[i].name;
                            string w_Stars = hotel[i].stars.ToString();
                            string w_NumberOfRooms = hotel[i].numberOfRooms.ToString();
                            save.Write("{0};{1};{2};{3}", w_Id, w_Name, w_Stars, w_NumberOfRooms);
                            //Edw apothikeueis ta reservations...
                            for (int j = 0; j < r_num[i]; j++)
                            {
                                string w_r_Name = hotel[i].reservations[j].name;
                                string w_CheckinDate = hotel[i].reservations[j].checkinDate.ToString();
                                string w_StayDurationDays = hotel[i].reservations[j].stayDurationDays.ToString();
                                save.Write(";{0};{1};{2}", w_r_Name, w_CheckinDate, w_StayDurationDays);
                            }
                            save.WriteLine(); //teleiwsa me to ksenodoxeio, allazw seira kai paw sto epomeno
                        }
                        //Telos tou save
                        save.Close();
                        saveflag = 0;
                        Console.WriteLine();
                        Console.WriteLine("Saving completed successfully!");
                    }
                    //Eisagwgi stoixeiwn
                    else if (choice == 3)
                    {
                        //Arxikopoiisi timwn
                        h_number = h_number + 1;
                        int[] n_r_num;
                        n_r_num = new int[1];
                        //Dimiourgeia enos deuterou object tou Hotel me idio megethos me to proigoumeno
                        Hotel[] n_hotel = new Hotel[h_number];
                        int n_Id = 0;
                        string n_Name = "";
                        int n_Stars = 0;
                        int n_NumberOfRooms = 0;
                        string n_r_Name = "";
                        int n_StayDurationDays = 0;
                        DateTime n_CheckinDate = DateTime.Parse("01/01/2000");
                        string data = "";
                        int accepted = 0;
                        //Eisagwgi ID hotel
                        Console.WriteLine();
                        Console.Write("Insert ID number: ");
                        data = Console.ReadLine();
                        n_Id = Int32.Parse(data);
                        data = "";
                        //Elegxos uparksis diplotupou ID
                        while (accepted == 0)
                        {
                            accepted = 1;
                            for (int i = 0; i < h_number - 1; i++)
                            {
                                if (n_Id == hotel[i].id)
                                {
                                    accepted = 0;
                                    Console.WriteLine();
                                    Console.WriteLine("ID number already exists..");
                                    Console.Write("Insert new ID number(Recommended bigger than '{0}'): ", hotel[h_number - 2].id);
                                    data = Console.ReadLine();
                                    n_Id = Int32.Parse(data);
                                    data = "";
                                }
                            }
                        }
                        //Eisagwgi epomenwn dedomenwn
                        Console.Write("Insert Hotel name: ");
                        n_Name = Console.ReadLine();
                        Console.Write("Insert number of stars: ");
                        data = Console.ReadLine();
                        n_Stars = Int32.Parse(data);
                        data = "";
                        Console.Write("Insert number of rooms: ");
                        data = Console.ReadLine();
                        n_NumberOfRooms = Int32.Parse(data);
                        data = "";
                        //Apothikeusi dedomenwn sto telos tou n_hotel
                        n_hotel[h_number - 1] = new Hotel(n_Id, n_Name, n_Stars, n_NumberOfRooms);
                        //Eisagwgi Reservation
                        Console.Write("Do you want to add any reservations to this hotel?(y/n)");
                        answer_2 = Console.ReadLine();
                        if (answer_2 == "y")
                        {
                            answer_2 = "n";
                            while (answer_3 == "y")
                            {
                                Console.Write("Insert customer name: ");
                                n_r_Name = Console.ReadLine();
                                Console.Write("Insert Check_In date: ");
                                data = Console.ReadLine();
                                n_CheckinDate = DateTime.Parse(data);
                                data = "";
                                Console.Write("Insert number of nights staying: ");
                                data = Console.ReadLine();
                                n_StayDurationDays = Int32.Parse(data);
                                data = "";
                                n_r_num[0]++;
                                n_hotel[h_number - 1].reservations.Add(new Reservation(n_r_Name, n_CheckinDate, n_StayDurationDays));

                                Console.Write("Do you want to add another reservation to this hotel?(y/n)");
                                answer_3 = Console.ReadLine();
                            }
                            //Allagi megethous tou arxikou mas object gia na dextei ta nea dedomena pou prosthesame kai metafora twn dedomenwn
                            Array.Resize(ref r_num, h_number);
                            Array.Copy(n_r_num, 0, r_num, h_number - 1, 1);
                            answer_3 = "y";
                        }
                        Array.Resize(ref hotel, h_number);
                        Array.Copy(n_hotel, h_number - 1, hotel, h_number - 1, 1);
                        saveflag = 1;
                    }
                    //Anazitisi me vasi to ID
                    else if (choice == 4)
                    {
                        //Taksinomisi stoixeiwn gia na ginei i anazitisi
                        BubbleSort(hotel);
                        //Arxikopoiisi timwn
                        string data = "";
                        int s_Id = 0;
                        int found = 0;
                        int w_found = 0;
                        int s_choice = GetSearchChoice();
                        //Elegxos periorismwn
                        while (s_choice < 1 || s_choice > 4)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid option!\nPlease enter a number between 1-4");
                            s_choice = GetSearchChoice();
                        }
                        //LINEAR SEARCH
                        if (s_choice == 1)
                        {
                            Console.WriteLine();
                            Console.Write("Insert ID number: ");
                            data = Console.ReadLine();
                            s_Id = Int32.Parse(data);
                            data = "";
                            for (int i = 0; i < h_number; i++)
                            {
                                if (hotel[i].id == s_Id)
                                {
                                    w_found = i;
                                    found = 1;
                                }
                            }
                        }
                        //BINARY SEARCH
                        else if (s_choice == 2)
                        {
                            Console.WriteLine();
                            Console.Write("Insert ID number: ");
                            data = Console.ReadLine();
                            s_Id = Int32.Parse(data);
                            data = "";
                            int left = 0;
                            int right = h_number - 1;
                            int middle = 0;
                            while (left <= right)
                            {
                                middle = (right + left / 2);
                                if (hotel[middle].id > s_Id) { right = middle - 1; }
                                else if (hotel[middle].id < s_Id) { left = middle + 1; }
                                else if (hotel[middle].id == s_Id)
                                {
                                    w_found = middle;
                                    found = 1;
                                    break;
                                }
                            }
                        }
                        //INTERPOLATION SEARCH
                        else if (s_choice == 3)
                        {
                            Console.WriteLine();
                            Console.Write("Insert ID number: ");
                            data = Console.ReadLine();
                            s_Id = Int32.Parse(data);
                            data = "";
                            int left = 0;
                            int right = h_number - 1;
                            int middle = 0;
                            while (hotel[left].id <= s_Id && hotel[right].id >= s_Id)
                            {
                                middle = left + ((s_Id - hotel[left].id) * (right - left)) / (hotel[right].id - hotel[left].id);
                                if (hotel[middle].id < s_Id)
                                {
                                    left = middle + 1;
                                }
                                else if (hotel[middle].id > s_Id)
                                {
                                    left = middle - 1;
                                }
                                else
                                {
                                    w_found = middle;
                                    found = 1;
                                    break;
                                }
                            }
                            if (hotel[left].id == s_Id)
                            {
                                w_found = left;
                                found = 1;
                            }
                        }
                        //AVL-TREE SEARCH
                        else if (s_choice == 4)
                        {
                            //TODO
                        }
                        //PRINTING
                        if (found == 1)
                        {
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Hotel found!\n");
                            Console.WriteLine("Name: {0}", hotel[w_found].name);
                            Console.WriteLine("Stars: {0}", hotel[w_found].stars);
                            Console.WriteLine("Number of rooms: {0}", hotel[w_found].numberOfRooms);
                            Console.WriteLine("--------------------------");
                        }
                        else if (found == 0)
                        {
                            Console.WriteLine("-----------------");
                            Console.WriteLine("Hotel not found!");
                            Console.WriteLine("-----------------");
                        }
                    }
                    //Anazitisi me vasi to onoma kratisis
                    else if (choice == 5)
                    {
                        string data = "";
                        int found_2 = 0;
                        int s_choice = GetSearchChoice_2();
                        //Elegxos periorismwn
                        while (s_choice < 1 || s_choice > 2)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid option!\nPlease enter a number between 1-2");
                            s_choice = GetSearchChoice_2();
                        }
                        //LINEAR SEARCH
                        if (s_choice == 1)
                        {
                            Console.WriteLine();
                            Console.Write("Insert customer name: ");
                            data = Console.ReadLine();
                            Console.WriteLine();
                            Console.WriteLine("Reservations done by: {0}", data);
                            Console.WriteLine("--------------------------------------");
                            for (int i = 0; i < h_number; i++)
                            {
                                for (int j = 0; j < r_num[i]; j++)
                                {
                                    if (hotel[i].reservations[j].name == data)
                                    {
                                        found_2 = 1;
                                        Console.WriteLine("Hotel: {0}", hotel[i].name);
                                        Console.WriteLine("Check_In date: {0}", hotel[i].reservations[j].checkinDate);
                                        Console.WriteLine("Number of nights staying: {0}", hotel[i].reservations[j].stayDurationDays);
                                        Console.WriteLine("--------------------------------------");
                                    }
                                }
                            }
                        }
                        else if (s_choice == 2)
                        {
                            //TODO
                        }
                        if (found_2 == 0)
                        {
                            Console.WriteLine("        No reservations done..        ");
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine();
                        }
                    }
                }
                //EXIT
                else if (choice == 6)
                {
                    if (saveflag == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You have unsaved data. Are you sure you want to exit?(y/n)");
                        answer = Console.ReadLine();
                    }
                    if (saveflag == 0 || answer == "y")
                    {
                        answer = "n";
                        Console.WriteLine();
                        Console.WriteLine("Have a nice day.");
                    }
                    else choice = 0;
                }
                else { Console.WriteLine(); Console.WriteLine("You need to load a file first!"); }
            } //END while(choice != 6)
        } //END main
    } //END class Running
}
