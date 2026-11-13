<?php

namespace App\Models;

enum ApiKeyIssuer: string
{
    case USER = 'USER';
    case SYSTEM = 'SYSTEM';
}