<?php

namespace App\Models;

enum SubscriptionStatus: string
{
    case ACTIVE = 'ACTIVE';
    case PAST_DUE = 'PAST_DUE';
    case INACTIVE = 'INACTIVE';
}