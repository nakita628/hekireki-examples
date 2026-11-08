<?php

namespace App\Models;

enum SubscriptionStatus: string
{
    case ACTIVE = 'ACTIVE';
    case TRIAL = 'TRIAL';
    case CANCELLED = 'CANCELLED';
    case EXPIRED = 'EXPIRED';
    case PAUSED = 'PAUSED';
}