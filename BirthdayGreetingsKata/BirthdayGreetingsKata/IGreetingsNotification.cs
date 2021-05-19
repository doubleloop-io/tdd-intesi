using System.Threading.Tasks;

namespace BirthdayGreetingsKata
{
    public interface IGreetingsNotification
    {
        Task SendForBirthday(Employee employee);
    }
}