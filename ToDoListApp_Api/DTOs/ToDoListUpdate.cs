/*
DTO :- Data Transfer Object 
Generally, DTOs classes describe what data come and out from the API

When updating a ToDoList, it only needs IsDone.
*/

using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.DTOs;

public class ToDoListUpdateDto
{
    [Required]
    public bool IsDone { get; set; }
}