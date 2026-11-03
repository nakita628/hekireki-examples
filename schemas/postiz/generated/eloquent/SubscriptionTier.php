<?php

namespace App\Models;

enum SubscriptionTier: string
{
    case STANDARD = 'STANDARD';
    case PRO = 'PRO';
    case TEAM = 'TEAM';
    case ULTIMATE = 'ULTIMATE';
}