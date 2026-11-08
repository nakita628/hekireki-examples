<?php

namespace App\Models;

enum CardVerificationStatus: string
{
    case PENDING = 'PENDING';
    case VERIFIED = 'VERIFIED';
    case REQUIRES_ACTION = 'REQUIRES_ACTION';
    case FAILED = 'FAILED';
}