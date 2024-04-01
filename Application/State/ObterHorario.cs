using Application.Models;
using Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.State;

public class ObterHorario : State
{
    public ObterHorario(TelegramDTO telegramDTO, IUnitOfWork unit) : base(telegramDTO, unit)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Edit(TelegramDTO.chatId, 
            new Request(){ME = TelegramDTO.TextoEnviado}, 
            nameof(ObterHorario));
    }

    public override async Task EnviarMensagem()
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "08:00", "10:00", "19:00", "21:00", "Cancelar" },
        })
        {
            ResizeKeyboard = true
        };

        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: "Qual o horário das visitas ?",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: TelegramDTO.cancellationToken);
    }
}