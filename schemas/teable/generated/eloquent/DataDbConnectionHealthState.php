<?php

namespace App\Models;

enum DataDbConnectionHealthState: string
{
    case healthy = 'healthy';
    case read_only = 'read_only';
    case unreachable = 'unreachable';
    case degraded = 'degraded';
}