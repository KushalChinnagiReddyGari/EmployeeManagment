using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManagement
{
    class Program
    {
        static String fPath = @"D:\Employee.txt";
     
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Employee Management System");
                Console.WriteLine("1. Add the Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. Update the Employee details");
                Console.WriteLine("4. Delete the Employee details");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        ViewEmployees();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddEmployee()
        {
            Console.WriteLine("Enter employee details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Department: ");
            string department = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(fPath, true))
            {
                writer.WriteLine($"{id},{name},{department}");
            }

            Console.WriteLine("Employee added successfully.");
        }

        static void ViewEmployees()
        {
            Console.WriteLine("Employee Details:");
            using (StreamReader reader = new StreamReader(fPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Console.WriteLine($"ID: {parts[0]}, Name: {parts[1]}, Department: {parts[2]}");
                }
            }
        }

        static void UpdateEmployee()
        {
            Console.Write("Enter the ID of the employee you want to update: ");
            int id = int.Parse(Console.ReadLine());
            List<string> lines = new List<string>();
            bool found = false;

            using (StreamReader reader = new StreamReader(fPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (int.Parse(parts[0]) == id)
                    {
                        Console.Write("Enter new name: ");
                        parts[1] = Console.ReadLine();
                        Console.Write("Enter new department: ");
                        parts[2] = Console.ReadLine();
                        line = string.Join(",", parts);
                        found = true;
                    }
                    lines.Add(line);
                }
            }

            if (found)
            {
                File.WriteAllLines(fPath, lines);
                Console.WriteLine("Employee details updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static void DeleteEmployee()
        {
            Console.Write("Enter the ID of the employee you want to delete: ");
            int id = int.Parse(Console.ReadLine());
            List<string> lines = new List<string>();
            bool found = false;

            using (StreamReader reader = new StreamReader(fPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (int.Parse(parts[0]) == id)
                    {
                        found = true;
                        continue; 
                    }
                    lines.Add(line);
                }
            }

            if (found)
            {
                File.WriteAllLines(fPath, lines);
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }
}