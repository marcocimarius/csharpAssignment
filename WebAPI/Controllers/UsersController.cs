using DTOs.Users;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUsersDTO request)
    {
        User user = new User(request.Username, request.Password);
        User created = await FileRepository.AddOneItemAsync(user);
        UserDTO dto = new()
        {
            Id = created.Id,
            Username = created.Username,
        };
        return Created($"/Users/{dto.Id}", created);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetUser([FromRoute] int id)
    {
        User? user = await FileRepository.ReadOneFromFileAsync<User>(id);

        if (user == null)
        {
            return NotFound($"User with id {id} not found");
        }

        UserDTO dto = new() { Id = user.Id, Username = user.Username };

        return Ok(dto);
    }

    [HttpGet("username")]
    public async Task<IResult> GetManyByUsername([FromQuery] string username)
    {
        List<User> users = await FileRepository.ReadFromFileAsync<User>();
        if (users.Count == 0)
        {
            return Results.NotFound("No users exist");
        }
        
        List<User> filteredUsers = users.Where(u => u.Username.Contains(username)).ToList();
        
        return filteredUsers.Count == 0 ? Results.NotFound("No users found") : Results.Ok(filteredUsers);
    }

    [HttpGet("all")]
    public async Task<IResult> GetAll()
    {
        List<User> users = await FileRepository.ReadFromFileAsync<User>();

        if (users.Count == 0)
        {
            return Results.NotFound("No users found");
        }
        
        return Results.Ok(users);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] int id, [FromBody] UpdateUsersDTO request)
    {
        User? user = await FileRepository.ReadOneFromFileAsync<User>(id);

        if (user == null)
        {
            return NotFound($"User with id {id} not found");
        }
        
        await FileRepository.RemoveOneItemAsync<User>(id);
     
        user.Username = request.Username;
        user.Password = request.Password;
        
        User updatedUser = await FileRepository.AddOneItemAsync(user);
        UserDTO dto = new() { Id = updatedUser.Id, Username = updatedUser.Username, };
        
        return Ok(dto);
    }

    [HttpDelete("{id:int}")]
    
    public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int id)
    {
        await FileRepository.RemoveOneItemAsync<User>(id);
        
        return NoContent();
    }
}