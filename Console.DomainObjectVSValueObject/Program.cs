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
            var result = Customer.Create(
                new PersonName("Max", "Mustermann"),
                new Email("max@test.de"),
                new Address("Hauptstraße 1", "1010",  "Entenhausen"));

            if (result.Success == false)
            {
                return;
            }

            Customer customer = result.Value!;

            customer.Rename(new PersonName("Max", "Meyer"));

            customer.ChangeEmail(new Email("meyer@test.de"));

            customer.Move(new Address("Bahnhofstraße 5", "68161", "Mannheim"));

            customer.Delete();

            foreach (var domainEvent in customer.DomainEvents)
            {
                switch (domainEvent)
                {
                    case CustomerCreated e:

                        Console.WriteLine(e.CustomerId);

                        break;

                    case CustomerRenamed e:

                        Console.WriteLine(e.Name.FirstName);
                        Console.WriteLine(e.Name.LastName);

                        break;

                    case CustomerEmailChanged e:

                        Console.WriteLine(e.Email.Value);

                        break;

                    case CustomerMoved e:

                        Console.WriteLine(e.Address.Street);
                        Console.WriteLine(e.Address.ZipCode);
                        Console.WriteLine(e.Address.City);

                        break;

                    case CustomerDeleted e:

                        Console.WriteLine(e.CustomerId);

                        break;
                }
            }

            Console.Wait();
        }
    }
}
