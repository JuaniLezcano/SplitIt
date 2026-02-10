using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.DTOs;
using SplitIt.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class QuickSplitController : ControllerBase
{
    private readonly IQuickSplitEqualService _service;

    public QuickSplitController(IQuickSplitEqualService service)
    {
        _service = service;
    }

    /// <summary>
    /// Calcula las deudas de un gasto rápido (Equal Split).
    /// </summary>
    [HttpPost]
    public ActionResult<List<QuickDebtDTO>> Post([FromBody] QuickExpenseDTO expense)
    {
        var debts = _service.CalculateDebts(expense);
        return Ok(debts);
    }
}
