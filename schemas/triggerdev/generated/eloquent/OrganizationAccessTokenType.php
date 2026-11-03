<?php

namespace App\Models;

enum OrganizationAccessTokenType: string
{
    case USER = 'USER';
    case SYSTEM = 'SYSTEM';
}