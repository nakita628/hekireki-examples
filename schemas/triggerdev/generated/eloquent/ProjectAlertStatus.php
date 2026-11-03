<?php

namespace App\Models;

enum ProjectAlertStatus: string
{
    case PENDING = 'PENDING';
    case SENT = 'SENT';
    case FAILED = 'FAILED';
}