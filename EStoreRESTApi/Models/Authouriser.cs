using EStoreRESTApi.Presenter;
using System;
using System.Collections.Generic;

namespace EStoreRESTApi.Models
{
    public class Authouriser
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Role { get; set; }

        public static string craeteCreditials(string username, string password, IAuthDataMapper authDataMapper = null)
        {
            if(authDataMapper == null )
            {
                authDataMapper = new AuthDataMapper();
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be empty");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password can not be empty");
            }

            if(authDataMapper.usernameExistInTheDatabase(username))
            {
                string message = string.Format("The username {0} is already registered", username);
                throw new ArgumentException(message);
            }

            if(!Validator.emailIsValid(username))
            {
                string message = string.Format("The email address {0} is invalid ", username);
                throw new ArgumentException(message);
            }
            int? results = authDataMapper.insertUserNewToDatabase(username, password);
            
            if( results > 0) return "Please check your email to complete your registeration";
            else return "Outch.. something went wrong, try again later";
        }
        public static List<Authouriser> getAllUsersCreditials(IAuthDataMapper authDataMapper = null)
        {
            List<Authouriser> results = new List<Authouriser>();
            if (authDataMapper == null)
            {
                authDataMapper = new AuthDataMapper();
            }

            results = authDataMapper.getAllUsersFromTheDatabase();

            if(results == null)
            {
                throw new ArgumentNullException("The system have no users registered currently, please reister");
            }
            else return results;
        }

        public static Authouriser getUserCreditials(string username, IAuthDataMapper authDataMapper = null)
        {
            Authouriser results = new Authouriser();
            if (authDataMapper == null)
            {
                authDataMapper = new AuthDataMapper();
            }

            results = authDataMapper.getUserFromTheDatabase(username);

            if (results == null)
            {
                string message = string.Format("The user {0} is not registered, please reister", username);
                throw new ArgumentNullException(message);
            }
            else return results;
        }

        public static string resetUserCreditials(string username, string password, IAuthDataMapper authDataMapper = null)
        {
            if (authDataMapper == null)
            {
                authDataMapper = new AuthDataMapper();
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be empty");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password can not be empty");
            }

            if (! authDataMapper.usernameExistInTheDatabase(username))
            {
                string message = string.Format("The username {0} is not registered", username);
                throw new ArgumentException(message);
            }

            if (!Validator.emailIsValid(username))
            {
                string message = string.Format("The email address {0} is invalid ", username);
                throw new ArgumentException(message);
            }
            int? results = authDataMapper.resetUserPasswordFromDatabase(username, password);

            if (results > 0) {
                string message = string.Format("Successfully updated user {0}", username);
                return message;
            }
            else
            {
                string message = string.Format("Failed to update user {0}", username);
                return message;
            }
        }

        // Unsubscribe user
        public static string unsubscribeUser(string username, string password, IAuthDataMapper authDataMapper = null)
        {
            if (authDataMapper == null)
            {
                authDataMapper = new AuthDataMapper();
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username can not be empty");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password can not be empty");
            }

            if (!authDataMapper.usernameExistInTheDatabase(username))
            {
                string message = string.Format("The username {0} is not registered", username);
                throw new ArgumentException(message);
            }
            return authDataMapper.deleteUserFromDatabase(username, password);
        }
    }
}
