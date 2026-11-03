<?php

namespace App\Models;

enum ReminderType: string
{
    case PENDING_BOOKING_CONFIRMATION = 'PENDING_BOOKING_CONFIRMATION';
}