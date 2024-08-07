using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace My_Bot
{
    public class BotRunner
    {
        private readonly TelegramBotClient Bot;
        private Task task ;

        public BotRunner(TelegramBotClient botClient)
        {
            this.Bot = botClient;
            Logger = new BotLogger(botClient);
        }

        public BotLogger Logger { get; }

        public async Task Run()
        {
            int offset = 0;
            while (true)
            {
                Update[] update = await Bot.GetUpdatesAsync(offset);

                foreach (var up in update)
                {
                    offset = up.Id + 1;

                    if (up.Message == null)
                        continue;

                    var id = up.Message.Chat.Id;
                    string? text = up.Message.Text?.ToLower();
                    var from = up.Message.From;
                    var messageId = up.Message.MessageId;

                    if (text == "/start")
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine("به بات اقا ایلیا خوش امدید" + " \U0001F643");

                        Bot?.SendTextMessageAsync(id, stringBuilder.ToString()/*, replyMarkup: MainKeyboard*/);
                        Logger.Log(id,messageId,from.Username,text,DateTime.Now, stringBuilder.ToString());
                    }
                    else if (text.Contains("جک") || text.Contains("جوک"))
                    {
                        var client = new HttpClient();
                        var response = await client.GetAsync("https://api3.haji-api.ir/majid/tools/jok/random?license=");
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();

                        var jsonObject = JObject.Parse(content);

                        var result = jsonObject["result"].ToString();

                        await Bot.SendTextMessageAsync(id, result, replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, result);

                    }
                    else if (text.Contains("مرسی") || text.Contains("ممنون") || text.Contains("تشکر"))
                    {
                        Bot?.SendTextMessageAsync(id, "خواهش میکنم", replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, "خواهش میکنم");
                    }
                    else if (text.Contains("\U0001F602") || text.Contains("\U0001F923"))
                    {
                        List<string> messages = new List<string>()
                        {
                            "جون تو فقط بخند",
                            "درد",
                            "مرض",
                        };

                        var random = new Random();
                        var randomResponse = messages[random.Next(messages.Count)];
                        Bot?.SendTextMessageAsync(id, randomResponse, replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, randomResponse);
                    }                  
                    else if (text.Contains("شب بخیر") || text.Contains("شب خوش") || text.Contains("شبخوش") || text.Contains("شو خوش"))
                    {
                        Bot?.SendTextMessageAsync(id, " بوس بوس لالا\U0001F618", replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, " بوس بوس لالا\U0001F618");
                    }
                    else if (text.Contains("قیمت دلار"))
                    {
                        var client = new HttpClient();
                        var response = await client.GetAsync("https://api4.haji-api.ir/api/tools/nobitex/?currency=rls&license=");
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            JsonElement result = root.GetProperty("result");
                            JsonElement usdtRls = result.GetProperty("usdt-rls");
                            string latestPrice = usdtRls.GetProperty("latest").GetString();                    

                            Bot?.SendTextMessageAsync(id, $"اخرین قیمت دلار : {latestPrice} ریال", replyToMessageId: messageId);
                            Logger.Log(id, messageId, from.Username, text, DateTime.Now, $"اخرین قیمت دلار : {latestPrice} ریال");
                        }
                    }
                    else if (text.Contains("قیمت بیتکوین") || text.Contains("قیمت بیت کوین"))
                    {
                        var client = new HttpClient();
                        var response = await client.GetAsync("https://api4.haji-api.ir/api/tools/nobitex/?currency=rls&license=");
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            JsonElement result = root.GetProperty("result");
                            JsonElement usdtRls = result.GetProperty("btc-rls");
                            string latestPrice = usdtRls.GetProperty("latest").GetString();                          

                            Bot?.SendTextMessageAsync(id, $"اخرین قیمت بیت کوین : {latestPrice} ریال", replyToMessageId: messageId);
                            Logger.Log(id, messageId, from.Username, text, DateTime.Now, $"اخرین قیمت بیت کوین : {latestPrice} ریال");
                        }
                    }
                    else if (text.Contains("انگیزه") || text.Contains("انگیزشی"))
                    {
                        var client = new HttpClient();
                        var response = await client.GetAsync("https://haji-api.ir/angizeshi/?license=");
                        response.EnsureSuccessStatusCode();

                        var result = await response.Content.ReadAsStringAsync();
                        Bot?.SendTextMessageAsync(id, result, replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, result);
                    }
                    else if (text.Contains("AI") || text.Contains("Ai") || text.Contains("ai") || text.Contains("aI"))
                    {
                        var client = new HttpClient();
                        var api = $"https://api3.haji-api.ir/majid/gpt/4?q={text.ToLower().Replace("ai", "").Trim()}&license=";
                        var response = await client.GetAsync(api);
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();

                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            string result = root.GetProperty("result").GetString();

                            var stringBuilder = new StringBuilder();
                            stringBuilder.AppendLine("هوش مصنوعی :");
                            stringBuilder.AppendLine("");
                            stringBuilder.AppendLine(result);
                            Bot?.SendTextMessageAsync(id, stringBuilder.ToString(), replyToMessageId: messageId);
                            Logger.Log(id, messageId, from.Username, text, DateTime.Now, stringBuilder.ToString());
                        }
                    }
                    else if (text.Contains("جمله سنگین"))
                    {
                        HttpClient httpClient = new HttpClient();
                        var response = await httpClient.GetAsync("https://haji-api.ir/gang?license=");
                        response.EnsureSuccessStatusCode();

                        var result = await response.Content.ReadAsStringAsync();
                        Bot?.SendTextMessageAsync(id, result, replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, result);
                    }
                    else if (text.Contains("خر"))
                    {
                        Bot?.SendTextMessageAsync(id, "خودت خری", replyToMessageId: messageId);
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, "خودت خری");
                    }
                    else
                    {
                        Logger.Log(id, messageId, from.Username, text, DateTime.Now, null);
                    }
                }
            }

        }

    }
}
