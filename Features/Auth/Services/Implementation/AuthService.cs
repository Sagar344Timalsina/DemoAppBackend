using DemoAppBE.Data;
using DemoAppBE.Domain;
using DemoAppBE.Features.Auth.DTOs;
using DemoAppBE.Features.Auth.Services.Interface;
using DemoAppBE.Infrastructure.Security;
using DemoAppBE.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoAppBE.Features.Auth.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private ApplicationDBContext _context { get; set; }
        private IPasswordHasher _hasher { get; set; }
        private ITokenProvider _tokenProvider { get; set; }
        public AuthService(ApplicationDBContext context, IPasswordHasher hasher, ITokenProvider tokenProvider)
        {
            _context = context;
            _hasher = hasher;
            _tokenProvider = tokenProvider;
        }
        public async Task<Result> generateRefreshTokenAsync(int UserId, string RefreshToken)
        {
            try
            {
                var user = _context.Users.Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role).FirstOrDefault(x => x.Id == UserId);

                if (user == null || user.RefreshToken != RefreshToken)
                {
                    return Result.Failure(Error.Failure("Refersh Token", "Invalid or expired refresh token"));
                }
                if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                {
                    return Result.Failure(Error.Unauthorize("Refersh Token", "Invalid or expired refresh token"));
                }
                var newAccessToken = _tokenProvider.CreateToken(user);
                var newRefreshToken = _tokenProvider.CreateRefereshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                user.UpdateLastModified();
                _context.Update(user);
                _context.SaveChanges();

                var loginResponseDto = new LoginResponseDTO
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                };

                return Result.Success(loginResponseDto);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("User.generateRefreshToken Failed", $"generateRefreshToken failed: {ex.Message}"));
            }
        }

        public async Task<Result> loginUserAsync(LoginRequestDTO loginRequestDto)
        {
            try
            {
                var existingUser = _context.Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role).FirstOrDefault(x => x.Email == loginRequestDto.Email);

                if (existingUser == null || !_hasher.Verify(loginRequestDto.Password, existingUser.Password))
                {
                    // Return a failure with the correct result type
                    return Result.Failure(Error.Failure("User.Failure", "Email or Password does not match!"));
                }

                var loginResponseDto = new LoginResponseDTO
                {
                    Token = _tokenProvider.CreateToken(existingUser),
                    RefreshToken = existingUser.RefreshToken
                };

                return Result.Success(loginResponseDto);
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("User.UserLoginFailed", $"Login failed: {ex.Message}"));

            }
        }

        public async Task<Result> registerUserAsync(RegisterRequestDTO registerRequestDto)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerRequestDto.Email);
                if (existingUser != null)
                {
                    return Result.Failure(Error.Conflict("User.Duplicate", "User with this email already exists."));
                }
                var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                if (defaultRole == null)
                {
                    return Result.Failure(Error.Problem("Role.NotFound", "The default 'User' role could not be found."));
                }
                User user = new();
                user.Password = _hasher.Hash(registerRequestDto.Password);
                user.RefreshToken = _tokenProvider.CreateRefereshToken();
                user.RefreshTokenExpiry = DateTime.Now.AddDays(7);
                user.Email = registerRequestDto.Email;
                user.UserName = registerRequestDto.UserName;


                _context.Add(user);
                await _context.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id
                };

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();


                return Result.Success("User registered successfully.");
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("User.RegistrationFailed", $"Registration failed: {ex.Message}"));
            }
        }

        public async Task<Result> getUserDetails(int Id)
        {
            try
            {
                var existingUser = _context.Users.Include(u => u.UserRoles)
               .ThenInclude(ur => ur.Role).FirstOrDefault(x => x.Id == Id);

                if (existingUser == null)
                {
                    // Return a failure with the correct result type
                    return Result.Failure(Error.Failure("User.Failure", "No User Found!"));
                }
                var userDto = new UserDTO
                {
                    Id = existingUser.Id,
                    UserName = existingUser.UserName,
                    Email = existingUser.Email,
                    UserRoles = existingUser.UserRoles.Select(ur => new UserRoleDTO
                    {
                        RoleId = ur.RoleId,
                        RoleName = ur.Role.Name
                    }).ToList()
                };
                return Result.Success(userDto);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("User.RegistrationFailed", $"Something went wrong: {ex.Message}"));
            }
        }
    }
}
