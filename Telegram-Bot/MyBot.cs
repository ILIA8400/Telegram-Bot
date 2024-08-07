using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace My_Bot
{
    public class MyBot
    {
        public MyBot(string token)
        {
            Bot = new TelegramBotClient(token);
            BotRunner = new BotRunner(Bot);
        }
        public TelegramBotClient Bot { get; }
        public ReplyKeyboardMarkup MainKeyboard { get; set; }
        public BotRunner BotRunner { get; }

        private async void BotStatus()
        {
            var botName = await Bot.GetMyNameAsync();
            
            Console.Write($"Bot Name is : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"<< { botName.Name} >>" );
            Console.ResetColor();

            Console.WriteLine("".PadLeft(50,'-'));

            Console.Write($"Bot status is : ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Online");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("----------------------- Logs -----------------------");
            Console.WriteLine();
        }

        private async Task Keyboard()
        {
            List<List<KeyboardButton>> keyboardButtons = new List<List<KeyboardButton>>();  
            keyboardButtons.Add(new List<KeyboardButton>() { new KeyboardButton("ارسال پیام به گروه"), new KeyboardButton("/start") });
            

            MainKeyboard = new ReplyKeyboardMarkup(keyboardButtons);

        }

        public async Task RunBot()
        {
            BotStatus();
            await Keyboard();
            await BotRunner.Run();
        }
    }
}
