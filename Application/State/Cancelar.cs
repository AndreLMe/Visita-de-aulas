using Application.Models;
using Application.State;
using Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

public class Cancelar : State
{
    public Cancelar(TelegramDTO telegramDTO, IUnitOfWork unit) : base(telegramDTO, unit)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Remove(TelegramDTO.chatId);
    }

    public override async Task EnviarMensagem()
    {
        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: "Cancelando...",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: TelegramDTO.cancellationToken);
    }
}