<?php

namespace App\Models;

enum TaskRunAttemptStatus: string
{
    case PENDING = 'PENDING';
    case EXECUTING = 'EXECUTING';
    case PAUSED = 'PAUSED';
    case FAILED = 'FAILED';
    case CANCELED = 'CANCELED';
    case COMPLETED = 'COMPLETED';
}