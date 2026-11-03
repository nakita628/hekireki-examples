<?php

namespace App\Models;

enum RoleType: string
{
    case SYSTEM = 'SYSTEM';
    case CUSTOM = 'CUSTOM';
}