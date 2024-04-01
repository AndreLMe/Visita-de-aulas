using Application.Models;
using Repository.Interfaces;

namespace Application.State;
public abstract class State
{
    protected TelegramDTO TelegramDTO {get;set;}
    protected IUnitOfWork UnitOfWork {get;set;}
    public State(TelegramDTO telegramDTO, IUnitOfWork unit)
    {
        TelegramDTO = telegramDTO;
        UnitOfWork = unit;
    }

    public abstract Task EnviarMensagem();
    public abstract void AtualizarDados();
}