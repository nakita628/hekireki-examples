<?php

namespace App\Models;

enum Role: string
{
    case ADMIN = 'ADMIN';
    case DEMO = 'DEMO';
    case INACTIVE = 'INACTIVE';
    case USER = 'USER';
}