<?php

namespace App\Models;

enum PlatformNotificationSurface: string
{
    case WEBAPP = 'WEBAPP';
    case CLI = 'CLI';
}