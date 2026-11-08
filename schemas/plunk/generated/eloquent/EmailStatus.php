<?php

namespace App\Models;

enum EmailStatus: string
{
    case SCHEDULED = 'SCHEDULED';
    case CANCELLED = 'CANCELLED';
    case PENDING = 'PENDING';
    case SENDING = 'SENDING';
    case SENT = 'SENT';
    case DELIVERED = 'DELIVERED';
    case RECEIVED = 'RECEIVED';
    case OPENED = 'OPENED';
    case CLICKED = 'CLICKED';
    case BOUNCED = 'BOUNCED';
    case COMPLAINED = 'COMPLAINED';
    case FAILED = 'FAILED';
}