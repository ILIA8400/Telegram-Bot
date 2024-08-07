using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace My_Bot
{
    public class BotLogger
    {
        private readonly TelegramBotClient botClient;

        public BotLogger(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public void Log(long chatId,int messageId,string? from,string text,DateTime date,string? response)
        {

            Console.WriteLine($"Date : {date.Year}/{date.Month}/{date.Day} {date.Hour}:{date.Minute} , ChatId :{chatId} , MessageId :{messageId}");
            if (from != null)
                Console.WriteLine($"From : {from}");
            Console.WriteLine();
            //if (text.Any(c => c >= 0x0600 && c <= 0x06FF))
            //{
            //    Console.WriteLine("Test : The message is Farsi");
            //}
            /*else*/ Console.WriteLine($"Text : {text}");
            Console.WriteLine();
            //if (response.Any(c => c >= 0x0600 && c <= 0x06FF))
            //{
            //    Console.WriteLine("Response : The message is Farsi");
            //}
            /*else*/ if(response != null) Console.WriteLine($"Response : {response}");

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine();

        }

       
    }
}
