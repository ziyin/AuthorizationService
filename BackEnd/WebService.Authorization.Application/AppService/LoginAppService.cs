using CustomerAuthorization.Interfaces;
using CustomerAuthorization.Models;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Login;
using WebService.Authorization.Application.Contracts.ResponseDtos.Login;
using WebService.Authorization.Domain.Login.Interfaces;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Domain.UserRole.Model;
using WebService.Authorization.Domain.UserRole.Model.Parameter;

namespace WebService.Authorization.Application.AppService;

public class LoginAppService
    (
    IValidateLoginManager validateLoginManager,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    IJwtTokenGenerator jwtTokenGenerator
    ) : ILoginAppService
{
    private readonly IValidateLoginManager _validateLoginManager = validateLoginManager;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<LoginDto> HandleAsync(LoginParameterDto parameterDto)
    {
        var resultDto = new LoginDto();
        var userData = await GetUserDataAsync(parameterDto.Account);
        if (userData == null)
        {
            return FailResult(resultDto, $"{parameterDto.Account} not exist.");
        }
        if (!IsPasswordValid(userData, parameterDto.Password, out var validationError))
        {
            return FailResult(resultDto, validationError!);
        }
        var userRole = await _userRoleRepository.GetListAsync(new GetUserRoleListParameterModel
        {
            UserId = userData.Id
        });
        resultDto.AccessToken = GenerateAccessToken(userData, userRole);
        return resultDto;
    }

    #region --Private

    private async Task<UserEntity?> GetUserDataAsync(string account)
    {
        return await _userRepository.GetAsync(new GetUserParameterModel { Account = account });
    }

    private bool IsPasswordValid(UserEntity userData, string password, out string? error)
    {
        error = _validateLoginManager.Handle(userData, password);
        return string.IsNullOrWhiteSpace(error);
    }

    private string GenerateAccessToken(UserEntity userData, IEnumerable<UserRoleDataModel>? userRoleDataModels)
    {
        var roles = userRoleDataModels is null ?
                    Enumerable.Empty<string>() :
                    userRoleDataModels.Select(item => item.RoleName);
        return _jwtTokenGenerator.GenerateToken(new GenerateTokenParameter
        {
            UserId = userData.Id.ToString(),
            UserName = userData.Name,
            Roles = roles
        });
    }

    private static LoginDto FailResult(LoginDto dto, string errorMessage)
    {
        dto.ErrorMessage = errorMessage;
        return dto;
    }
    #endregion
}