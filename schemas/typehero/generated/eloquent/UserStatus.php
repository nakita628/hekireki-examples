<?php

namespace App\Models;

enum UserStatus: string
{
    case BANNED = 'BANNED';
    case ACTIVE = 'ACTIVE';
}