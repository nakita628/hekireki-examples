<?php

namespace App\Models;

enum InsightState: string
{
    case active = 'active';
    case suppressed = 'suppressed';
    case closed = 'closed';
}