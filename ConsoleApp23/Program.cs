using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp21
{
    interface IObserver
    {
        void Update(string info);
    }
    class FileLoggerObserver : IObserver
    {
        public void Update(string info)
        {
            Console.WriteLine($"{info} for File Logger");//file a yaz
        }
    }
    class MailSenderObserver : IObserver
    {
        public void Update(string info)
        {
            Console.WriteLine($"{info} for Mail Sender");//camaatin mailine gonder
        }
    }
    class Observable
    {
        List<IObserver> observers = new List<IObserver>();
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public bool RemoveObserver(IObserver observer)
        {
            return observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(" . . . Some Information . . . ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Observable observable = new Observable();
            FileLoggerObserver observer = new FileLoggerObserver();
            MailSenderObserver observer2 = new MailSenderObserver();
            observable.AddObserver(observer); observable.AddObserver(observer2);
            observable.Notify();

        }
    }
}




