<?php

namespace App\Models;

enum BookingReportStatus: string
{
    case PENDING = 'PENDING';
    case DISMISSED = 'DISMISSED';
    case BLOCKED = 'BLOCKED';
}