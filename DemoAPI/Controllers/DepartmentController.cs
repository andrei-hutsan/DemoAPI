using DataAccess.Interfaces;
using DemoAPI.DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers;

/// <summary>
/// CRUD implementation
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public DepartmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Returns all departments
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return Ok(ApiResponse<List<DepartmentModel>>.Succeed(departments.ToList()));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<DepartmentModel>>.Fail(
                "An error occurred while fetching departments.",
                new List<string> { ex.Message }
            ));
        }
    }


    /// <summary>
    /// Returns a specific department by ID
    /// </summary>
    /// <param name="id">Department ID</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null || department.Id == Guid.Empty)
                return NotFound(ApiResponse<DepartmentModel>.Fail("Department not found."));

            return Ok(ApiResponse<DepartmentModel>.Succeed(department));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<DepartmentModel>.Fail("An error occurred while retrieving the department.", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Adds a new department
    /// </summary>
    /// <param name="department">New department object</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DepartmentModel department)
    {
        try
        {
            if (department == null)
                return BadRequest(ApiResponse<string>.Fail("Invalid department object."));

            await _unitOfWork.Departments.AddAsync(department);
            return Ok(ApiResponse<string>.Succeed("Department added successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("An error occurred while adding the department.", new List<string> { ex.Message }));
        }
    }

    /// <summary>
    /// Deletes a department by ID
    /// </summary>
    /// <param name="id">Department ID</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound(ApiResponse<string>.Fail("Department not found."));

            await _unitOfWork.Departments.DeleteAsync(id);
            return Ok(ApiResponse<string>.Succeed("Department deleted successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("An error occurred while deleting the department.", new List<string> { ex.Message }));
        }
    }
}
