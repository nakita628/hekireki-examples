<?php

namespace App\Models;

enum GatewayConnectionStatus: string
{
    case ENABLED = 'enabled';
    case DISABLED = 'disabled';
    case ERROR = 'error';
}