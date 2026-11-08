<?php

namespace App\Models;

enum ReportStatus: string
{
    case PENDING = 'PENDING';
    case CLEARED = 'CLEARED';
    case DISMISSED = 'DISMISSED';
}