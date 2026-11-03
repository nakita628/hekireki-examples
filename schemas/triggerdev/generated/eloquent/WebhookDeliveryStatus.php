<?php

namespace App\Models;

enum WebhookDeliveryStatus: string
{
    case PENDING = 'PENDING';
    case PROCESSING = 'PROCESSING';
    case SUCCEEDED = 'SUCCEEDED';
    case FAILED = 'FAILED';
    case FILTERED = 'FILTERED';
    case UNMATCHED = 'UNMATCHED';
}