<?php

namespace App\Models;

enum SMSLockState: string
{
    case LOCKED = 'LOCKED';
    case UNLOCKED = 'UNLOCKED';
    case REVIEW_NEEDED = 'REVIEW_NEEDED';
}