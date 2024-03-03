using Application.State;

namespace Context;
public class StateContext
{
    private State State {get;set;}

    public StateContext(State state)
    {
        State = state;
        //State.SetContext(this);
    }
    // public async Task AtualizarDados()
    // {
    //     await State.AtualizarDados();
    // }
    // public async Task EnviarNovaMensagem()
    // {
    //     await State.EnviarNovaMensagem();
    // }
    // public async Task EnviarMensagem()
    // {
    //     await State.EnviarMensagem();
    // }
}