<?php

namespace App\Models;

enum WatchlistType: string
{
    case EMAIL = 'EMAIL';
    case DOMAIN = 'DOMAIN';
    case USERNAME = 'USERNAME';
}