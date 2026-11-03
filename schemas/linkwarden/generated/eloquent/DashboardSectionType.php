<?php

namespace App\Models;

enum DashboardSectionType: string
{
    case STATS = 'STATS';
    case RECENT_LINKS = 'RECENT_LINKS';
    case PINNED_LINKS = 'PINNED_LINKS';
    case COLLECTION = 'COLLECTION';
}