<?php

namespace App\Models;

enum SubscriptionInterval: string
{
    case month = 'month';
    case year = 'year';
}