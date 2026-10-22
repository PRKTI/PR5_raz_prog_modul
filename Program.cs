using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PR5_raz_prog_modul.Models;
using HashPasswords;

namespace PR5_raz_prog_modul
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создание новой учетной записи для пользователя\n");

            Console.Write("Введите ФИО пользователя: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();

            Console.Write("Введите Email: ");
            string email = Console.ReadLine();

            Console.Write("Введите логин пользователя: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль пользователя: ");
            string password = Console.ReadLine();

            string hashedPass = Hash.HashPassword(password);

            Console.WriteLine($"Хешированный пароль пользователя: {hashedPass}");

            try
            {
                using (Computer_Company_FixDBEntities db = new Computer_Company_FixDBEntities())
                {
                    persons newPerson = new persons()
                    {
                        full_name = fullName,
                        phone = phone,
                        email = email
                    };

                    db.persons.Add(newPerson);
                    db.SaveChanges();

                    users newUser = new users()
                    {
                        id_person = newPerson.id_person,
                        login = login,
                        password_hash = hashedPass,
                        role = "Клиент"
                    };

                    db.users.Add(newUser);
                    db.SaveChanges();
                }

                Console.WriteLine("Учетная запись добавлена");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении записи: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}