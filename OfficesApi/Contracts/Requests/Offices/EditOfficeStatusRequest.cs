using System.ComponentModel.DataAnnotations;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Contracts.Requests.Offices;

public class EditOfficeStatusRequest
{
    public Guid Id { get; set; }
    [Range(0, 1)] 
    public OfficeStatus Status { get; set; }
}