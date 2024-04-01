using Application.Models;
using Application.State;
using Context;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


Utils.FilesUtil.LoadEnvFile();
var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_TOKEN"));

var uow = new UnitOfWork(new AulasContext());
using CancellationTokenSource cts = new ();

ReceiverOptions receiverOptions = new ()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
uow.Dispose();
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    State estado;

    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    
    var dto = new TelegramDTO{
        botClient = botClient,
        update = update,
        cancellationToken = cancellationToken,
        TextoEnviado = messageText,
        chatId = chatId
    };
    
    if(!uow.Requests.Contains(chatId))
    {
        estado = new ObterUniversidade(dto, uow);
    }
    
    else if(messageText.ToLower() == "cancelar")
    {
        estado = new Cancelar(dto, uow);
    }
    
    else
    {
        var key = await uow.Requests.Get(chatId);
        
        if(String.IsNullOrEmpty(key?.EstadoAnterior))
        {
            uow.Requests.Remove(chatId);
            return;
        }

        switch (key.EstadoAnterior){
            case "ObterUniversidade":
                estado = new ObterCampi(dto, uow);
                break;
            case "ObterCampi":
                estado = new ObterME(dto, uow);
                break;
            case "ObterME":
                estado = new ObterHorario(dto, uow);
                break;
            case "ObterHorario":
                estado = new EnviarSalas(dto, uow);
                break;
            default:
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Adeus pessoa linde :D",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                
                if(uow.Requests.Contains(chatId))
                    uow.Requests.Remove(chatId);
                
                return;
        }
    }

    await estado.EnviarMensagem();
    estado.AtualizarDados();
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
