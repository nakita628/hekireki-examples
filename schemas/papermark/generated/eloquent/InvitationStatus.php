<?php

namespace App\Models;

enum InvitationStatus: string
{
    case SENT = 'SENT';
    case FAILED = 'FAILED';
    case BOUNCED = 'BOUNCED';
}