<?php

namespace App\Models;

enum TrackingMode: string
{
    case ENABLED = 'ENABLED';
    case DISABLED = 'DISABLED';
    case MARKETING_ONLY = 'MARKETING_ONLY';
}