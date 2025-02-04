using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class ContractController : ApiController
{
    public ContractController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateContract([FromBody] Command.CreateContractCommand createContract)
    {
        var result = await Sender.Send(createContract);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ContractResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllContract(string? SearchTerm = null, int? ContracNumber = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetContractsQuery(SearchTerm, ContracNumber, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{contractId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateContract(Guid contractId, [FromBody] Command.UpdateContractCommand updateContract)
    {
        var updateContractCommand = new Command.UpdateContractCommand(contractId, updateContract.ContracNumber, updateContract.SignDate, updateContract.EffectiveDate, updateContract.ExpirationDate, updateContract.EmployeeId);
        var result = await Sender.Send(updateContractCommand);
        return Ok(result);
    }

    //[HttpPut()]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CheckContractExpiration()
    //{
    //    var result = await Sender.Send(new Command.CheckContractNearExpiration());
    //    return Ok(result);
    //}
}
