<?php

namespace App\Models;

enum StepExecutionStatus: string
{
    case PENDING = 'PENDING';
    case SCHEDULED = 'SCHEDULED';
    case WAITING = 'WAITING';
    case RUNNING = 'RUNNING';
    case COMPLETED = 'COMPLETED';
    case SKIPPED = 'SKIPPED';
    case FAILED = 'FAILED';
}