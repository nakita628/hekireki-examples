<?php

namespace App\Models;

enum JobExecutionStatus: string
{
    case COMPLETED = 'COMPLETED';
    case ERROR = 'ERROR';
    case PENDING = 'PENDING';
    case CANCELLED = 'CANCELLED';
    case DELAYED = 'DELAYED';
}