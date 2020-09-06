using System.Threading.Tasks;
using MicroRabbit.Mvc.Models.Dto;

namespace MicroRabbit.Mvc.Services
{
    public interface ITransferService
    {
        Task Transfer(TransferDto transferDto);
    }
}
