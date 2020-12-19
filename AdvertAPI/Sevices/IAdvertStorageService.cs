using AdvertAPI.Models;
using System.Threading.Tasks;

namespace AdvertAPI.Sevices
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task Confirm(ConfirmAdvertModel model);
    }
}
