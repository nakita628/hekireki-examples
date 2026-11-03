<?php

namespace App\Models;

enum DocumentVisibility: string
{
    case EVERYONE = 'EVERYONE';
    case MANAGER_AND_ABOVE = 'MANAGER_AND_ABOVE';
    case ADMIN = 'ADMIN';
}