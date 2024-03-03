using System.Globalization;
using System.Text;
using Application.Models;
using Repository.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Universidade;

namespace Application.State;

public class EnviarSalas : State
{
    public EnviarSalas(TelegramDTO telegramDTO, IUnitOfWork unit) : base(telegramDTO, unit)
    {
    }

    public override void AtualizarDados()
    {
        UnitOfWork.Requests.Remove(TelegramDTO.chatId);
    }

    private void PreencherHorario(){
        UnitOfWork.Requests.Edit(TelegramDTO.chatId, 
            new Request(){Horario = TelegramDTO.TextoEnviado}, 
            nameof(EnviarSalas));
    }

    public override async Task EnviarMensagem()
    {
        PreencherHorario();
        var req = await UnitOfWork.Requests.Get(TelegramDTO.chatId);
        
        if(req is null)
            return;
        
        string hoje = DateTime.Today
        .ToString("dddd", new CultureInfo("pt-BR"))
        .Split('-').First();

        var results = await UnitOfWork.Aulas.GetPorHorarioECampi(req.Horario, req.Campi, hoje);
        
        switch(req.ME){
            case "CABCT":
                results = results
                .Where(t => 
                t.curso == "BACHARELADO EM CIÊNCIA E TECNOLOGIA")
                .ToList();
                break;
            case "DALI":
                results = results
                .Where(t => 
                t.curso.Contains("LICENCIATURA"))
                .ToList();
                break;
        }

        StringBuilder sb = new StringBuilder();
        
        results = results
                    .Where(t=> 
                        !t.sala.Contains("L") &&
                        UFABCSalasWhiteList.SalasVisitaveis
                        .Exists(c => c.Equals(t.sala)))
                        .ToList();
        results.ForEach(t => sb.AppendLine($"{t.nomeDaMateria} - {t.sala}"));
        
        if(String.IsNullOrEmpty(sb.ToString()))
        {
            sb = sb.Clear();
            sb.AppendLine("Não foram encontradas salas para hoje.");
        }

        Console.WriteLine(sb.ToString());
            
        await TelegramDTO.botClient.SendTextMessageAsync(
            chatId: TelegramDTO.chatId,
            text: sb.ToString(),
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: TelegramDTO.cancellationToken);
    }

    public override Task EnviarNovaMensagem()
    {
        throw new NotImplementedException();
    }
}