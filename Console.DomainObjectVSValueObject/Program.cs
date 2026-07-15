//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2026
// </copyright>
// <Template>
// 	Version 3.0.2026.2, 15.04.2026
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>26.06.2026 11:03:08</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

namespace Console.DomainObjectVSValueObject
{
    /* Imports from NET Framework */
    using System;

    using DDD_Demo;

    using DDDFW;

    public class Program
    {
        public Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }

        private static void Main(string[] args)
        {
            CMenu mainMenu = new CMenu("DDD Demo");
            mainMenu.AddItem("DDD Customer", MenuPoint1);
            mainMenu.AddItem("Beenden", () => ApplicationExit());
            mainMenu.Show();
            
        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            Console.Clear();

            var nameResult = PersonName.Create("Max","Mustermann");

            if (nameResult.Success == false)
            {
                return;
            }

            var emailResult = Email.Create("max@test.de");

            if (emailResult.Success == false)
            {
                return;
            }

            var addressResult = Address.Create("Hauptstraße 1", "1010", "Entenhausen");

            if (addressResult.Success == false)
            {
                return;
            }

            var customerResult = Customer.Create(
                nameResult.Value!,
                emailResult.Value!,
                addressResult.Value!);

            if (customerResult.Success == false)
            {
                return;
            }

            Customer customer = customerResult.Value!;

            customer.Rename("Dagobert", "Duck");

            customer.ChangeEmail("dagobert.duck@entenhausen.eh");

            customer.Move("Talerstrasse 1", "1010", "Entenhausen");

            customer.Delete();

            var resultDelete = customer.Delete();
            if (resultDelete.Success == false)
            {
                Console.WriteLine(resultDelete.Errors.FirstOrDefault());
            }

            foreach (var domainEvent in customer.DomainEvents)
            {
                switch (domainEvent)
                {
                    case CustomerCreated e:
                        Console.Line();
                        Console.WriteLine($"Erstellt Customer mit Id: {e.CustomerId}");

                        break;

                    case CustomerRenamed e:

                        Console.Line();
                        Console.WriteLine("Rename Person");
                        Console.WriteLine(e.Name.FirstName);
                        Console.WriteLine(e.Name.LastName);
                        Console.WriteLine(customer.CreatedOn);

                        break;

                    case CustomerEmailChanged e:
                        Console.Line();
                        Console.WriteLine("Change Email");
                        Console.WriteLine(e.Email.Value);
                        Console.WriteLine(customer.ModifiedOn);

                        break;

                    case CustomerMoved e:
                        Console.Line();
                        Console.WriteLine("Geändert Adresse");
                        Console.WriteLine(e.Address.Street);
                        Console.WriteLine(e.Address.ZipCode);
                        Console.WriteLine(e.Address.City);
                        Console.WriteLine(customer.ModifiedOn);

                        break;

                    case CustomerDeleted e:
                        Console.Line();
                        Console.WriteLine("Customer gelöscht");
                        Console.WriteLine(e.CustomerId);
                        Console.WriteLine(customer.ModifiedOn);

                        break;
                }
            }

            Console.Wait();
        }
    }
}
