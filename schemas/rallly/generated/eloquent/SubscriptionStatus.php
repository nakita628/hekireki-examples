<?php

namespace App\Models;

enum SubscriptionStatus: string
{
    case incomplete = 'incomplete';
    case incomplete_expired = 'incomplete_expired';
    case active = 'active';
    case paused = 'paused';
    case trialing = 'trialing';
    case past_due = 'past_due';
    case canceled = 'canceled';
    case unpaid = 'unpaid';
}