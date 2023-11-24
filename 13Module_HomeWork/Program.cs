using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13Module_HomeWork
{
    class Program
    {
        static void Main()
        {
            // Создаем объект класса BankQueue, представляющий систему управления очередью в банке
            BankQueue bankQueue = new BankQueue();

            // Бесконечный цикл для взаимодействия с пользователем
            while (true)
            {
                // Выводим меню действий
                Console.WriteLine("1. Встать в очередь");
                Console.WriteLine("2. Обслужить следующего клиента");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите действие: ");

                // Получаем выбор пользователя
                string choice = Console.ReadLine();

                // Обрабатываем выбор пользователя
                switch (choice)
                {
                    case "1":
                        // Пользователь встает в очередь, вводит ИИН и выбирает тип услуги
                        Console.Write("Введите ИИН клиента: ");
                        string id = Console.ReadLine();
                        Console.Write("Выберите услугу (1 - Кредитование, 2 - Открытие вклада, 3 - Консультация): ");
                        ServiceType serviceType = (ServiceType)Enum.Parse(typeof(ServiceType), Console.ReadLine());
                        bankQueue.Enqueue(new Client(id, serviceType));
                        break;

                    case "2":
                        // Администратор обслуживает следующего клиента в очереди
                        bankQueue.ServeNextClient();
                        break;

                    case "3":
                        // Завершаем программу при выборе выхода
                        return;

                    default:
                        // Обработка некорректного ввода
                        Console.WriteLine("Некорректный ввод. Повторите попытку.");
                        break;
                }
            }
        }
    }

    // Перечисление, представляющее типы услуг
    public enum ServiceType
    {
        Credit,
        Deposit,
        Consultation
    }

    // Класс, представляющий клиента
    public class Client
    {
        public string Id { get; }
        public ServiceType ServiceType { get; }

        // Конструктор для создания клиента с указанным ИИН и типом услуги
        public Client(string id, ServiceType serviceType)
        {
            Id = id;
            ServiceType = serviceType;
        }

        // Переопределение метода ToString для удобного вывода информации о клиенте
        public override string ToString()
        {
            return $"Клиент {Id}, Услуга: {ServiceType}";
        }
    }

    // Класс, представляющий очередь в банке
    public class BankQueue : IEnumerable<Client>
    {
        private Queue<Client> queue = new Queue<Client>();

        // Метод для добавления клиента в очередь
        public void Enqueue(Client client)
        {
            queue.Enqueue(client);
            Console.WriteLine($"Клиент {client.Id} добавлен в очередь. Текущая длина очереди: {queue.Count}");
        }

        // Метод для обслуживания следующего клиента в очереди
        public void ServeNextClient()
        {
            if (queue.Count > 0)
            {
                Client servedClient = queue.Dequeue();
                Console.WriteLine($"Обслужен клиент: {servedClient}");
                Console.WriteLine($"Текущая длина очереди: {queue.Count}");
            }
            else
            {
                Console.WriteLine("Очередь пуста.");
            }
        }

        // Реализация интерфейса IEnumerable для поддержки цикла foreach
        public IEnumerator<Client> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        // Реализация неявного интерфейса IEnumerable (нужна для поддержки цикла foreach)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
