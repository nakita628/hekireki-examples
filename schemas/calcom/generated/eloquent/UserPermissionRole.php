<?php

namespace App\Models;

enum UserPermissionRole: string
{
    case USER = 'USER';
    case ADMIN = 'ADMIN';
}