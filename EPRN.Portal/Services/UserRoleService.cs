﻿using EPRN.Common.Enums;
using EPRN.Portal.Services.Interfaces;

namespace EPRN.Portal.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly List<UserRole> _roles = new List<UserRole>();

        public bool HasRole(UserRole roleToTestFor)
        {
            return _roles.Contains(roleToTestFor);
        }

        public void RemoveRole(UserRole userRole)
        {
            _roles.Remove(userRole);
        }

        public void SetRole(UserRole userRole)
        {
            if (userRole == UserRole.None)
                return;

            if (!_roles.Contains(userRole))
            {
                _roles.Add(userRole);
            }
        }
    }
}
