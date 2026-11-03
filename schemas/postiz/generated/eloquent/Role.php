<?php

namespace App\Models;

enum Role: string
{
    case SUPERADMIN = 'SUPERADMIN';
    case ADMIN = 'ADMIN';
    case USER = 'USER';
}