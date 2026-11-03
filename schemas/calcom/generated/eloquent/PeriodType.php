<?php

namespace App\Models;

enum PeriodType: string
{
    case UNLIMITED = 'unlimited';
    case ROLLING = 'rolling';
    case ROLLING_WINDOW = 'rolling_window';
    case RANGE = 'range';
}