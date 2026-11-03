<?php

namespace App\Models;

enum ActionExecutionStatus: string
{
    case COMPLETED = 'COMPLETED';
    case ERROR = 'ERROR';
    case PENDING = 'PENDING';
    case CANCELLED = 'CANCELLED';
}