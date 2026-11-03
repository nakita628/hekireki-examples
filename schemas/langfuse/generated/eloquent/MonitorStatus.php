<?php

namespace App\Models;

enum MonitorStatus: string
{
    case PAUSED = 'PAUSED';
    case ACTIVE = 'ACTIVE';
    case ERROR_BAD_QUERY = 'ERROR_BAD_QUERY';
}