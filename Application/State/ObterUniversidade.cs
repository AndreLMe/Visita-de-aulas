using Application.Models;
using Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.State;
public class ObterUniversidade : State
{
    public ObterUniversidade(TelegramDTO telegramDTO, IUnitOfWork unitOfWork) : base(telegramDTO, unitOfWork)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Add(TelegramDTO.chatId, new Request(){EstadoAnterior = nameof(ObterUniversidade)});
    }

    public override async Task EnviarMensagem()
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "UFABC", "Cancelar" },
        })
        {
            ResizeKeyboard = true
        };

        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: "Escolha a universidade:",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: TelegramDTO.cancellationToken);
    }
}