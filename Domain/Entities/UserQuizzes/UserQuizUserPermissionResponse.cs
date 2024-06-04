﻿using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserQuizzes;
public record UserQuizUserPermissionResponse(int UserId, EUserPermission Permission, string FirstName, string LastName, string Username);
