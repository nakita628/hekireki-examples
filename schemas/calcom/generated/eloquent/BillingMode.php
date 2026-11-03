<?php

namespace App\Models;

enum BillingMode: string
{
    case SEATS = 'SEATS';
    case ACTIVE_USERS = 'ACTIVE_USERS';
}