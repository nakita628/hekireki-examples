<?php

namespace App\Models;

enum PhoneNumberSubscriptionStatus: string
{
    case ACTIVE = 'ACTIVE';
    case PAST_DUE = 'PAST_DUE';
    case CANCELLED = 'CANCELLED';
    case INCOMPLETE = 'INCOMPLETE';
    case INCOMPLETE_EXPIRED = 'INCOMPLETE_EXPIRED';
    case TRIALING = 'TRIALING';
    case UNPAID = 'UNPAID';
}