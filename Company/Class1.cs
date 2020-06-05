//temat 7 zad1
//Dla klasy Employee przeciąż domyślne działanie operatora == pozwalające na porównanie dwóch pracowników, jako porównanie ich imion i nazwisk.
//temat 7 zad2
//Dla klasy Employee przeciąż domyślne działanie operatorów < i > pozwalające na porównanie dwóch pracowników, jako porównanie ich wynagrodzeń.
//temat 7 zad3
//Zdefiniuj operator konwersji z typu Employee na double, powodujący zwrócenie jako wyniku wysokości pensji.
//temat 7 zad4
//Dla klasy Employee przeciąż domyślne działanie operatora + jako pozwalające na dodanie do pracownika liczby. Jako wynik zwracana jest suma pensji pracownika i podanej liczby.

//temat 8 zad1
//Zdefiniuj w klasie Employee delegację pozwalającą podczepiać metody wywoływane, jeśli zmieni się imię lub nazwisko pracownika. Napisz kod wywołujący delegację, jeśli nastąpi zmiana imienia lub nazwiska.
//temat 8 zad2
//Zdefiniuj metodę wypisującą, że nastąpiła zmiana nazwiska lub imienia. Przypisz ją do delegacji. Sprawdź czy delegacja działa, zmieniając dla pracownika najpierw imię o następnie nazwisko.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company2
{
    //temat1/zad6-7
    namespace Finances.Employees
    {
        //temat5zad1
        public class EmployeeFactory
        {

            public static Employee CreateEmployee()
            {
                return new Employee();
            }
            public static Employee CreateEmployee(string name, string surname, string contract, string position)
            {
                return new Employee(name, surname, contract, position);
            }
        }

        //temat1/zad1
        //temat 6 zad1
        //Efekt: klasa jest zadeklarowana jako abstrakcyjna, przez musi posiadać niezdefiniowaną metodę, trzeba tą metodę zdefiniować w klasie, która dziedziczy. Wtedy będzie można stworzyć obiekt.
        public abstract class Person
        {

            public string Name { get; set; }
            public string Surname { get; set; }

            //temat1/zad5
            public string PersonalData() { string temp = String.Join(" ", Name, Surname); return temp; }
            public void ChangePersonalData(string name, string surname)
            {
                Name = name;
                Surname = surname;
            }
            //temat 6 zad2
            //Odp: Tak jest to możliwe, ze względu na to że klasa jest abstrakcyjna, jednakże trzeba zdefiniować metodę w klasie dziedziczącej.
            public void Interests(string Interest) { }
            //temat2/zad1
            public Person(string name, string surname)
            {
                Name = name;
                Surname = surname;
            }
            //temat1/zad9
            public Person()
            {
                Name = "Anonymous";
                Surname = "Anonymouse";
            }
        }
        //temat1/zad1
        public class Employee : Person
        {
            //temat1/zad2
            public static double HolidayBonus { get; set; } = 1000;
            //temat 3 zad 1
            public static List<Operation> Operation = new List<Operation>();
            //temat1/zad3
            static Wage wageStruct = new Wage(2000, 0, HolidayBonus);
            private double wage = wageStruct.Basic + wageStruct.Bonus + wageStruct.Other;
            //temat2/zad1
            public double Wage
            {
                get => wage;
                set
                {
                    Console.WriteLine("Podaj Login: ");
                    string loginTemp = Console.ReadLine();
                    if (loginTemp == "Admin")
                    {
                        Console.WriteLine("Podaj Hasło: ");
                        string passwordTemp = Console.ReadLine();
                        if (passwordTemp == "Password") { Console.WriteLine("Wage został zmieniony na: " + value); wage = value; }
                    }
                    else { Console.WriteLine("Błędny Login! "); Console.ReadKey(); }
                }
            }
            public int ID { get; set; }
            public string Position { get; set; }
            //temat1/zad4
            private string contract;
            public string Contract
            {
                get => contract;
                set
                {
                    bool exists = Enum.IsDefined(typeof(allowedContracts), value);
                    if (exists) { contract = value; }
                    else
                    {
                        Console.WriteLine("Invalid Contract!!");
                        foreach (string s in Enum.GetNames(typeof(allowedContracts)))
                            Console.WriteLine("Allowed contract: " + s);
                        Console.ReadKey();
                        contract = "InvalidContractName";
                    }
                }
            }

            //temat8/zad1,zad2
            public void ChangeEmployeeName(string name, string surname)
            {
                Name = name;
                Surname = surname;
                _changeSomething("Imię zostało zmienione.");
            }
            //temat8/zad1,zad2
            public delegate void ChangeSomething(string message);
            //temat8/zad1,zad2
            private ChangeSomething _changeSomething;
            //temat8/zad1,zad2
            public void AddCallback(ChangeSomething msg)
            {
                _changeSomething += msg;
            }

            //temat9/zad1
            public delegate void SalaryHasChanged();
            public event SalaryHasChanged ChangeSalary;
            protected virtual void OnSalaryChange()
            {
                if (ChangeSalary != null)
                    ChangeSalary();
                else
                    Console.WriteLine($"New Salary: {Wage}");
            }
            public void SalaryChange(int newWage)
            {
                Console.WriteLine($"OldSalary; {Wage}");
                Wage = newWage;
                OnSalaryChange();
            }
            //temat7/zad1
            public static bool operator ==(Employee employee1, Employee employee2)
            {
                if ((employee1.Name == employee2.Name) && (employee1.Surname == employee2.Surname))
                    return true;
                else
                    return false;
            }
            //temat7/zad1
            public static bool operator !=(Employee employee1, Employee employee2)
            {
                if ((employee1.Name != employee2.Name) && (employee1.Surname != employee2.Surname))
                    return true;
                else
                    return false;
            }
            //temat7/zad2
            public static bool operator <(Employee employee1, Employee employee2)
            {
                bool status = employee1.wage > employee2.wage;
                return status;
            }
            //temat7/zad2
            public static bool operator >(Employee employee1, Employee employee2)
            {
                bool status = employee1.wage > employee2.wage;
                return status;
            }
            //temat7/zad3
            public static implicit operator double(Employee employee)
            {
                return (double)employee.wage;
            }
            //zadanie7/zad4
            public static int operator +(Employee employee, int number)
            {
                return (int)employee.wage + number;
            }


            //temat1/zad5
            public string EmployeeData() { string temp = String.Join(" ", PersonalData(), Position, Contract, wage); return temp; }
            //temat2/zad1
            public void ChangeEmployeeData(string name, string surname, string position)
            {
                ChangePersonalData(name, surname);
                Position = position;
                Contract = contract;
            }
            public void ChangeHolidayBonus(int holidaybonus) { HolidayBonus = holidaybonus; }
            public void ChangeWage(double wage, double bonus, double other)
            { wageStruct.Basic = wage; wageStruct.Bonus = bonus; wageStruct.Other = other; wage = (wageStruct.Basic + wageStruct.Bonus + wageStruct.Other); }
            public void Payout(string nazwaOperacji)
            {
                string details = "Dokonano Wypłaty Panu/i: " + PersonalData() + " o kwocie: " + Wage;
                Console.WriteLine(details); new Operation(nazwaOperacji, details);
            }
            //temat1/zad9
            public Employee(string name, string surname, string contract, string position, double wage = 2000, double wageBonus = 1000, double wageOther = 0) : base(name, surname)
            {
                Position = position;
                Contract = contract;
                wageStruct.Basic = wage;
                wageStruct.Bonus = wageBonus;
                wageStruct.Other = wageOther;
                listOfEmployees.Add(this);
            }
            public Employee(string name, string surname, string contract, double wage = 2000, double wageBonus = 1000, double wageOther = 0) : base(name, surname)
            {
                Position = "Undefined";
                Contract = contract;
                wageStruct.Basic = wage;
                wageStruct.Bonus = wageBonus;
                wageStruct.Other = wageOther;
                listOfEmployees.Add(this);
            }
            public Employee() { listOfEmployees.Add(this); }
            private List<Employee> listOfEmployees = new List<Employee>();
            //temat5/zad2
            public void AddEmployee()
            {
                listOfEmployees.Add(EmployeeFactory.CreateEmployee());
            }

            //temat5/zad3
            public void AddEmployeeDemo()
            {
                var id = listOfEmployees.Count + 1;
                Console.WriteLine("Enter First Name: ");
                var firstName = Console.ReadLine();
                Console.WriteLine("Enter last name: ");
                var lastName = Console.ReadLine();
                Console.WriteLine("Enter Contract");
                var contract = Console.ReadLine();
                Console.WriteLine("Enter position");
                var position = Console.ReadLine();

                foreach (var employee in listOfEmployees)
                {
                    if (employee.Name == firstName && employee.Surname == lastName)
                        Console.WriteLine("User Already Exists.");
                    return;
                }
                listOfEmployees.Add(EmployeeFactory.CreateEmployee(firstName, lastName, contract, position));
            }
            public void RemoveEmployee()
            {

                ShowEmployees();
                Console.Write("Enter ID to delete employee: ");
                var id = Convert.ToInt32(Console.ReadLine());
                listOfEmployees.Remove(listOfEmployees.Where(employee => employee.ID == id).First());
            }

            public void ShowEmployees()
            {
                Console.WriteLine("List of employees");
                foreach (var employee in listOfEmployees)
                {
                    Console.WriteLine($"Name: { employee.Name} Last Name: {employee.Surname}");
                }
            }
            public Employee GetEmployee()
            {
                ShowEmployees();
                Console.WriteLine("Enter Employee id: ");
                var id = Convert.ToInt32(Console.ReadLine());
                return listOfEmployees.First(x => x.ID == id);
            }

        }

        //temat 3 zad 1
        public class Employees
        {
            // temat 4 zad 1
            private string[] _tableOfFirstLastName = new string[100];
            public string this[int i]
            {
                get { return _tableOfFirstLastName[i]; }
                set { _tableOfFirstLastName[i] = value; }
            }

            public static List<Employee> ListofEmployees = new List<Employee>();
            public void NewEmployee(Employee employee) { ListofEmployees.Add(employee); }
            public List<Employee> EmployeeList() { return ListofEmployees; }
        }

        //temat1/zad1
        public class Operation
        {
            //temat1/zad2
            public static List<Operation> operations = new List<Operation>();
            private int id = 1;
            public int Id { get => id; }
            public string Name { get; set; }
            private DateTime date = DateTime.Now;
            public DateTime Date { get => date; }
            public string Other { get; set; }
            //temat1/zad5
            public string OperationsList()
            {
                StringBuilder list = new StringBuilder();
                foreach (Operation operation in operations)
                    list.Append("Operacja Numer: " + operation.Id + " " + operation.Other + " Data Operacji: " + operation.Date);
                return list.ToString();
            }
            public Operation() { }
            //temat1/zad9
            public Operation(string name, string data)
            {
                Name = name;
                id++;
                Other = data;
                operations.Add(this);
            }
        }
        //temat1/zad10
        public class Client : Person
        {
            public Client(string name, string surname) : base(name, surname)
            {

            }
        }
        //temat1/zad10
        public class Manager : Employee
        {
            public Manager(string name, string surname, string contract, string position) : base(name, surname, contract, position)
            {

            }
        }
        //temat1/zad3
        public struct Wage
        {
            public double Basic { get; set; }
            public double Bonus { get; set; }
            public double Other { get; set; }
            public Wage(double basic, double bonus, double other)
            {
                Basic = basic;
                Bonus = bonus;
                Other = other;
            }
        }

    }

    //temat1/zad4
    public enum allowedContracts { FullTime, PartTime, Contract };

}