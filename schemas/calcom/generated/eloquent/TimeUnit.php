<?php

namespace App\Models;

enum TimeUnit: string
{
    case DAY = 'day';
    case HOUR = 'hour';
    case MINUTE = 'minute';
}