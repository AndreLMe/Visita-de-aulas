using Application.Models;
using Repository.Interfaces;

namespace Application.State;
public abstract class State
{
    //protected StateContext StateContext {get;set;}
    protected TelegramDTO TelegramDTO {get;set;}
    protected IUnitOfWork UnitOfWork {get;set;}
    public State(TelegramDTO telegramDTO, IUnitOfWork unit)
    {
        TelegramDTO = telegramDTO;
        UnitOfWork = unit;
    }

    // public void SetContext(StateContext stateContext)
    // {
    //     StateContext = stateContext;
    // }

    public abstract Task EnviarMensagem();
    public abstract void AtualizarDados();
    public abstract Task EnviarNovaMensagem();
}