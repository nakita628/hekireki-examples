<?php

namespace App\Models;

enum MockServerAction: string
{
    case CREATED = 'CREATED';
    case DELETED = 'DELETED';
    case ACTIVATED = 'ACTIVATED';
    case DEACTIVATED = 'DEACTIVATED';
}