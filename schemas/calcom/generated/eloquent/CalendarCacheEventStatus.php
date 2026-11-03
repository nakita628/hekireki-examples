<?php

namespace App\Models;

enum CalendarCacheEventStatus: string
{
    case confirmed = 'confirmed';
    case tentative = 'tentative';
    case cancelled = 'cancelled';
}