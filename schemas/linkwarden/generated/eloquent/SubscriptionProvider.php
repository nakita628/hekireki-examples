<?php

namespace App\Models;

enum SubscriptionProvider: string
{
    case STRIPE = 'STRIPE';
    case APPLE = 'APPLE';
    case GOOGLE = 'GOOGLE';
}