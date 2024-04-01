
using Application.Models;
using Repository.Interfaces;
using Telegram.Bot;

using Telegram.Bot.Types.ReplyMarkups;

namespace Application.State;

public class ObterCampi : State
{
    public ObterCampi(TelegramDTO telegramDTO, IUnitOfWork unit) : base(telegramDTO, unit)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Edit(TelegramDTO.chatId, 
            new Request(){Universidade = TelegramDTO.TextoEnviado}, 
            nameof(ObterCampi));
    }

    public override async Task EnviarMensagem()
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Santo André", "São Bernardo do Campo", "Cancelar" },
        })
        {
            ResizeKeyboard = true
        };
    
        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: "Selecione o campi:",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: TelegramDTO.cancellationToken);
    }
}