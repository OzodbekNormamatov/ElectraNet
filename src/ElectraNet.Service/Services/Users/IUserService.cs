using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Users;

namespace ElectraNet.Service.Services.Users;

public interface IUserService
{
    /// <summary>
    /// Creates a new usern based on the provided user creation model.
    /// </summary>
    /// <param name="createModel">The user creation model containing information about the user to create.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing a <see cref="UserViewModel"/> representing the created user.</returns>
    ValueTask<UserViewModel> CreateAsync(UserCreateModel createModel);

    /// <summary>
    /// Updates an existing user based on the specified ID and the provided user update model.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="updateModel">The user update model containing the new information for the user.</param>
    /// <param name="IsUserDeleted">A boolean indicating whether the user is marked as deleted (optional).</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing a <see cref="UserViewModel"/> representing the updated user.</returns>
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel, bool IsUserDeleted = false);

    /// <summary>
    /// Deletes a user based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a user based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.
    ValueTask<UserViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all users based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting users.</param>
    /// <param name="search">The optional search term to filter users.
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    /// <summary>
    /// Authenticates a user based on the provided phone number and password, and returns a user object along with an authentication token.
    /// </summary>
    /// <param name="phone">The phone number of the user to authenticate.</param>
    /// <param name="password">The password of the user to authenticate.
    ValueTask<(UserViewModel user, string token)> LoginAsync(string phone, string password);

    /// <summary>
    /// Resets the password for a user based on the provided phone number and new password.
    /// </summary>
    /// <param name="phone">The phone number of the user whose password needs to be reset.</param>
    /// <param name="newPassword">The new password for the user.
    ValueTask<bool> ResetPasswordAsync(string phone, string newPassword);

    /// <summary>
    /// Sends a verification code to the provided phone number for user verification.
    /// </summary>
    /// <param name="phone">The phone number to which the verification code will be sent.
    ValueTask<bool> SendCodeAsync(string phone);

    /// <summary>
    /// Verifies the provided verification code against the provided phone number for user authentication.
    /// </summary>
    /// <param name="phone">The phone number for which the verification code was sent.</param>
    /// <param name="code">The verification code to verify.
    ValueTask<bool> ConfirmCodeAsync(string phone, string code);

    /// <summary>
    /// Changes the password for a user based on the provided phone number, old password, and new password.
    /// </summary>
    /// <param name="phone">The phone number of the user whose password needs to be changed.</param>
    /// <param name="oldPassword">The old password of the user.</param>
    /// <param name="newPassword">The new password for the user.
    ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword);
}