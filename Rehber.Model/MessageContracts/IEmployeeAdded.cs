using Rehber.Model.DataModels;

namespace Rehber.Model.MessageContracts
{
    public interface IEmployeeAdded
    {
        Employees Employee { get; }
    }
}