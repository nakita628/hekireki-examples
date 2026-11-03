<?php

namespace App\Models;

enum WatchlistSource: string
{
    case MANUAL = 'MANUAL';
    case FREE_DOMAIN_POLICY = 'FREE_DOMAIN_POLICY';
    case SIGNUP = 'SIGNUP';
}