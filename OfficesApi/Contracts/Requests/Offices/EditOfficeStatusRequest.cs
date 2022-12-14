using System.ComponentModel.DataAnnotations;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Contracts.Requests.Offices;

public class EditOfficeStatusRequest
{
    [Range(0,1)]
    public OfficeStatus Status { get; set; }
}