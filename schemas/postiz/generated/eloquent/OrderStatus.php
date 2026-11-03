<?php

namespace App\Models;

enum OrderStatus: string
{
    case PENDING = 'PENDING';
    case ACCEPTED = 'ACCEPTED';
    case CANCELED = 'CANCELED';
    case COMPLETED = 'COMPLETED';
}