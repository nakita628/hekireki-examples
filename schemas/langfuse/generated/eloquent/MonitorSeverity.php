<?php

namespace App\Models;

enum MonitorSeverity: string
{
    case PAUSED = 'PAUSED';
    case UNKNOWN = 'UNKNOWN';
    case NO_DATA = 'NO_DATA';
    case OK = 'OK';
    case WARNING = 'WARNING';
    case ALERT = 'ALERT';
}