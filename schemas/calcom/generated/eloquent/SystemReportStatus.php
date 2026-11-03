<?php

namespace App\Models;

enum SystemReportStatus: string
{
    case PENDING = 'PENDING';
    case BLOCKED = 'BLOCKED';
    case DISMISSED = 'DISMISSED';
}