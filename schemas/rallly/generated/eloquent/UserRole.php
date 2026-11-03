<?php

namespace App\Models;

enum UserRole: string
{
    case admin = 'admin';
    case user = 'user';
}