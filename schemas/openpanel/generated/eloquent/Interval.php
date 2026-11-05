<?php

namespace App\Models;

enum Interval: string
{
    case hour = 'hour';
    case day = 'day';
    case month = 'month';
    case minute = 'minute';
    case week = 'week';
}