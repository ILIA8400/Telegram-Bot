using My_Bot;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Enter the Token Api:  ");
string token = Console.ReadLine();

MyBot bot = new(token);
await bot.RunBot();

