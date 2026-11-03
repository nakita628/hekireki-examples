<?php

namespace App\Models;

enum WebhookDeliveryStatus: string
{
    case pending = 'pending';
    case in_flight = 'in_flight';
    case succeeded = 'succeeded';
    case failed = 'failed';
    case exhausted = 'exhausted';
}