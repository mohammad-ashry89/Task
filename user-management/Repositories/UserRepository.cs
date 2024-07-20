using Microsoft.EntityFrameworkCore;
using user_management.Data;
using user_management.DTOs.User;
using user_management.Interfaces;
using user_management.Models;

namespace user_management.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IGroupRepository _groupRepo;
        public UserRepository(ApplicationDBContext context, IGroupRepository groupRepo)
        {
            _context=context;
            _groupRepo=groupRepo;
        }

        public async Task<User> addUserAsync(User u)
        {
            await _context.users.AddAsync(u);
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<User> assignGroup(string username, string groupName)
        {
            var user= await getUserByUsernameAsync(username);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            var group=await _groupRepo.getGroupByName(groupName);
            if(group == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            
            user.groupId=group.groupId;
            _context.SaveChanges();
            return user;
        }

        public async Task< User?> deleteUser(string username)
        {
            var user= await _context.users.FirstOrDefaultAsync(u=> u.UserName == username); 
            if(user==null)
            {
                return null;
            }
            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public  async Task<List<User>> getAllUsersAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User?> getUserByUsernameAsync(string username)
        {
            var user = await _context.users.FirstOrDefaultAsync(u=>u.UserName==username);
            return user;
        }

        public async Task<User?> updateUser(string username, UserUpdateDto dto)
        {
            var user= await _context.users.FirstOrDefaultAsync(u=>u.UserName == username);
            if(user==null)
            {
                return null;
            }
            if(dto.firstName!=null && dto.firstName!="")
        user.firstName=dto.firstName;
        if(dto.lastName!=null && dto.lastName!="")
        user.lastName=dto.lastName;
        if(dto.email!=null && dto.email!="")
        user.Email=dto.email;
         await _context.SaveChangesAsync();
        return user;
        }
    }
}