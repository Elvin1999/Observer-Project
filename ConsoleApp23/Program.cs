using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public FileLoggerObserver(string ownerName)
        {
            OwnerName = ownerName;
        }
        public string OwnerName { get; set; }
        public void Update(string info)
        {
            WriteToFile(info);
            Console.WriteLine("====================================================");
            Console.WriteLine($"Info sent to {OwnerName} and to File Succesfully");
            Console.WriteLine("====================================================");
            //ReadFromFile();
        }
        public void WriteToFile(string info)
        {
            using (StreamWriter wr = new StreamWriter("file.json", true))
            {
                wr.WriteLine($"{info} for {OwnerName} and to File");
            }
        }
        public void ReadFromFile()
        {
            using (StreamReader sr = new StreamReader("file.json"))
            {
                var result = sr.ReadToEnd();
                Console.WriteLine("====================================================");
                Console.WriteLine(result);
                Console.WriteLine("====================================================");
            }        
        }
    }
    class MailSenderObserver : IObserver
    {
        public MailSenderObserver(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
        public string EmailAddress { get; set; }
        public void Update(string info)
        {
            try
            {
                string mailBodyhtml =
                "<h1>Some information for MailSender</h1>";
                var msg = new MailMessage("camalzade_elvin@mail.ru", EmailAddress, "Hello", mailBodyhtml);
                msg.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.mail.ru", 587);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("camalzade_elvin@mail.ru", "ugurluimtahan");
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
                Console.WriteLine("====================================================");
                Console.WriteLine($"Email Sended to {EmailAddress} Successfully");
                Console.WriteLine("====================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            Console.ForegroundColor = ConsoleColor.Green;
            Observable observable = new Observable();
            FileLoggerObserver observer1 = new FileLoggerObserver("John");
            FileLoggerObserver observer2 = new FileLoggerObserver("Mike");
            FileLoggerObserver observer3 = new FileLoggerObserver("Brian");
            MailSenderObserver Samir = new MailSenderObserver("samirhemzeyev2001@gmail.com");
            MailSenderObserver Elvin = new MailSenderObserver("camalzade_elvin@mail.ru");
            MailSenderObserver Samir2 = new MailSenderObserver("samirhemzeyev2001@gmail.com");
            MailSenderObserver Elvin2 = new MailSenderObserver("camalzade_elvin@mail.ru");
            observable.AddObserver(observer1);
            observable.AddObserver(observer2);
            observable.AddObserver(observer3);
            observable.AddObserver(Samir); observable.AddObserver(Samir2);
            observable.AddObserver(Elvin); observable.AddObserver(Elvin2);
            observable.Notify();

        }
    }
}




