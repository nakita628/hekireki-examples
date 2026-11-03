<?php

namespace App\Models;

enum WatchlistAction: string
{
    case REPORT = 'REPORT';
    case BLOCK = 'BLOCK';
    case ALERT = 'ALERT';
}