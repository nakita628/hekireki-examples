<?php

namespace App\Models;

enum GatewayIngestionMode: string
{
    case NONE = 'none';
    case USAGE = 'usage';
    case FULL = 'full';
}