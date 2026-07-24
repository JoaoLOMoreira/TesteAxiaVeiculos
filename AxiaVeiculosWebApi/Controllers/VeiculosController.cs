using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Queries;
using AxiaVeiculosApplication.Veiculos.Requests;
using AxiaVeiculosApplication.Veiculos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AxiaVeiculosWebApi.Controllers;

[ApiController]
[Route("api/veiculos")]
public sealed class VeiculosController : ControllerBase
{
    private readonly ISender _sender;

    public VeiculosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VeiculoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var veiculo = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(ObterPorId), new { id = veiculo.Id }, veiculo);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarVeiculoRequest request, CancellationToken cancellationToken)
    {
        var command = new AtualizarVeiculoCommand(
            id,
            request.Descricao,
            request.Marca,
            request.Modelo,
            request.Opcionais,
            request.Valor);

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VeiculoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeiculoResponse>> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var veiculo = await _sender.Send(new ObterVeiculoPorIdQuery(id), cancellationToken);

        return Ok(veiculo);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<VeiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyCollection<VeiculoResponse>>> Listar(CancellationToken cancellationToken)
    {
        var veiculos = await _sender.Send(new ListarVeiculosQuery(), cancellationToken);

        return Ok(veiculos);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await _sender.Send(new ExcluirVeiculoCommand(id), cancellationToken);

        return NoContent();
    }
}
