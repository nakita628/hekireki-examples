<?php

namespace App\Models;

enum SupportAccessRequestStatus: string
{
    case PENDING = 'PENDING';
    case APPROVED = 'APPROVED';
    case CANCELLED = 'CANCELLED';
}