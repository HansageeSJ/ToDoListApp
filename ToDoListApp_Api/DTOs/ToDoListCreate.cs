/*
DTO :- Data Transfer Object 
Generally, DTOs classes describe what data come and out from the API

When creating a ToDoList, it only needs Title.
*/

using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.DTOs;

public class ToDoListCreateDto
{
    [Required, MinLength(1), MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
}