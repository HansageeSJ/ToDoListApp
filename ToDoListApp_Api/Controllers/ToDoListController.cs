using Microsoft.AspNetCore.Mvc;
using ToDoListApp.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly IToDoListService _service;

    public ToDoListController(IToDoListService service) => _service = service;

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<TodoItem> Get(Guid id) => _service.Get(id) is { } item ? Ok(item) : NotFound();

    [HttpPost]
    public ActionResult<TodoItem> Create([FromBody] ToDoListCreateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var created = _service.Add(dto.Title);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ToDoListUpdateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        return _service.Update(id, dto.IsDone) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) => _service.Delete(id) ? NoContent() : NotFound();
    
}