
using Application.Models;
using Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.State;

public class ObterME : State
{
    public ObterME(TelegramDTO telegramDTO, IUnitOfWork unit) : base(telegramDTO, unit)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Edit(TelegramDTO.chatId, 
            new Request(){Campi = TelegramDTO.TextoEnviado}, 
            nameof(ObterME));
    }

    public override async Task EnviarMensagem()
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "DCE", "CABCT", "DALI", "Cancelar" },
        })
        {
            ResizeKeyboard = true
        };

        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: "Entidade ?",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: TelegramDTO.cancellationToken);
    }
}