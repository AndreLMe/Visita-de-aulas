using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Models;

public class TelegramDTO
{
    public ITelegramBotClient botClient {get;set;}
    public Update update {get;set;}
    public CancellationToken cancellationToken {get;set;}
    public long chatId {get;set;}
    public string TextoEnviado { get; set; }
}