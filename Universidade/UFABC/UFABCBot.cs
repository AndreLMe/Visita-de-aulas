using System.Globalization;
using System.Text;
using Context;
using Repository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Universidade.UFABCs;

public static class UFABCBot
{
    public static async Task BuscarCampi(ITelegramBotClient botClient, 
    Update update, CancellationToken cancellationToken, long chatId)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] { "Santo André", "São Bernardo do Campo" },
    })
    {
        ResizeKeyboard = true
    };

    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Selecione o campi:",
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: cancellationToken);
    }

    public static async Task BuscaMU(ITelegramBotClient botClient, 
    Update update, CancellationToken cancellationToken, long chatId)
    {
        if(System.IO.File.Exists($"./logs/{chatId}")){
            System.IO.File.Delete($"./logs/{chatId}");
        }
        using(var sr = new StreamWriter($"./logs/{chatId}", true)){
            sr.WriteLine(update.Message?.Text);
        }
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "DCE", "CABCT", "DALI" },
        })
        {
            ResizeKeyboard = true
        };

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Entidade ?",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    public static async Task BuscaHorario(ITelegramBotClient botClient, 
    Update update, CancellationToken cancellationToken, long chatId)
    {
        using(var sr = new StreamWriter($"./logs/{chatId}", true)){
            sr.WriteLine(update.Message?.Text);
        }
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "08:00", "10:00", "19:00", "21:00" },
        })
        {
            ResizeKeyboard = true
        };

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Qual o horário das visitas ?",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    public static async Task BuscaTurmas(ITelegramBotClient botClient, 
    Update update, CancellationToken cancellationToken, long chatId)
    {
        string campi;
        string MU;
        string? horario = update.Message?.Text;
        using(var sr = new StreamReader($"./logs/{chatId}", true)){
            var txt = sr.ReadToEnd().Split('\n');
            campi = txt[0];
            MU = txt[1];
        }

        var dbContext = new AulasContext();
        var rep = new AulasRepository(dbContext);
        string hoje = DateTime.Today
        .ToString("dddd", new CultureInfo("pt-BR"))
        .Split('-').First();

        var results = await rep.GetPorHorarioECampi(horario, campi, hoje);
        
        Console.WriteLine();

        switch(MU){
            case "CABCT":
                results = results
                .Where(t => 
                t.curso == "BACHARELADO EM CIÊNCIA E TECNOLOGIA")
                .ToList();
                break;
            case "DALI":
                results = results
                .Where(t => 
                t.curso == "LICENCIATURA EM CIÊNCIAS HUMANAS" ||
                t.curso == "LICENCIATURA EM CIÊNCIAS NATURAIS E EXATAS")
                .ToList();
                break;
        }


        StringBuilder sb = new StringBuilder();
        System.IO.File.Delete($"./logs/{chatId}");
        results = results.Where(t=> !t.sala.Contains("L")).ToList();
        results.ForEach(t => sb.AppendLine($"{t.nomeDaMateria} - {t.sala}"));
        Console.WriteLine(sb.ToString());
        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: sb.ToString(),
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}