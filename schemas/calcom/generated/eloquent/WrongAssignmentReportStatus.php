<?php

namespace App\Models;

enum WrongAssignmentReportStatus: string
{
    case PENDING = 'PENDING';
    case REVIEWED = 'REVIEWED';
    case RESOLVED = 'RESOLVED';
    case DISMISSED = 'DISMISSED';
}