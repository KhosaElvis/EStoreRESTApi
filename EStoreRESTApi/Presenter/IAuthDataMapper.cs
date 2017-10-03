using EStoreRESTApi.Models;
using System.Collections.Generic;

namespace EStoreRESTApi.Presenter
{
    public interface IAuthDataMapper
    {
        bool usernameExistInTheDatabase(string username);
        int? insertUserNewToDatabase(string username, string password, string defaultRole = "Client");
        List<Authouriser> getAllUsersFromTheDatabase();
        Authouriser getUserFromTheDatabase(string username);
        string deleteUserFromDatabase(string username, string password);
        int? resetUserPasswordFromDatabase(string username, string password);
    }
}
