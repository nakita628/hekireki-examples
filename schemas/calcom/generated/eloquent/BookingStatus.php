<?php

namespace App\Models;

enum BookingStatus: string
{
    case CANCELLED = 'cancelled';
    case ACCEPTED = 'accepted';
    case REJECTED = 'rejected';
    case PENDING = 'pending';
    case AWAITING_HOST = 'awaiting_host';
}