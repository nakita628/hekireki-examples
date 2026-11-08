<?php

namespace App\Models;

enum RoleTypes: string
{
    case USER = 'USER';
    case ADMIN = 'ADMIN';
    case MODERATOR = 'MODERATOR';
    case CREATOR = 'CREATOR';
    case SUPPORTER = 'SUPPORTER';
    case CONTRIBUTOR = 'CONTRIBUTOR';
}