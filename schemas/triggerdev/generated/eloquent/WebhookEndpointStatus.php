<?php

namespace App\Models;

enum WebhookEndpointStatus: string
{
    case ACTIVE = 'ACTIVE';
    case INACTIVE = 'INACTIVE';
    case DELETING = 'DELETING';
}