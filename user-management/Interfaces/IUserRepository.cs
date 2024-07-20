using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user_management.DTOs.User;
using user_management.Models;

namespace user_management.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> getAllUsersAsync();
        Task<User?> getUserByUsernameAsync(string username);
        Task<User> addUserAsync(User u);
        Task<User?> updateUser(String username,UserUpdateDto dto);
        Task<User?> deleteUser(String username);
        Task<User> assignGroup(string username, string groupName);
    }
}