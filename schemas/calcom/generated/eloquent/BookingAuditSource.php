<?php

namespace App\Models;

enum BookingAuditSource: string
{
    case API_V1 = 'api_v1';
    case API_V2 = 'api_v2';
    case WEBAPP = 'webapp';
    case WEBHOOK = 'webhook';
    case SYSTEM = 'system';
    case MAGIC_LINK = 'magic_link';
    case UNKNOWN = 'unknown';
}