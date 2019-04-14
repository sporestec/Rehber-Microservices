using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;

namespace Rehber.Model.MessageContracts
{
    public interface IEmployeeAdded
    {
        EmployeeViewModel Employee { get; }
    }
}