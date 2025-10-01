using Azure.Core;
using DemoAppBE.Features.Auth.DTOs;
using DemoAppBE.Features.Auth.Services.Interface;
using DemoAppBE.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace DemoAppBE.Features.Auth.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IValidator<RegisterRequestDTO> _regValidator, IUserContext _userContext,IValidator<LoginRequestDTO> _loginValidator, IAuthService _authsService) : ControllerBase
    {

        [HttpPost("register-user")]
        public async Task<IActionResult> registerUser(RegisterRequestDTO registerRequestDTO)
        {

            var validationResult = await _regValidator.ValidateAsync(registerRequestDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var result = Result.Failure(Error.Failure("Validation.Error", string.Join("; ", errors)));
                return BadRequest(result);
            }
            var res = await _authsService.registerUserAsync(registerRequestDTO);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> loginUser(LoginRequestDTO loginRequestDTO)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginRequestDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var result = Result.Failure(Error.Failure("Validation.Error", string.Join("; ", errors)));
                return BadRequest(result);
            }
            var res = await _authsService.loginUserAsync(loginRequestDTO);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpGet("Logged-User-Details")]
        [Authorize]
        public async Task<IActionResult> myDetails()
        {
            int Id = _userContext.Id;
            var res = await _authsService.getUserDetails(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
    }
}
