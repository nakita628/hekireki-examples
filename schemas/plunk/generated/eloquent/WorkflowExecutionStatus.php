<?php

namespace App\Models;

enum WorkflowExecutionStatus: string
{
    case RUNNING = 'RUNNING';
    case WAITING = 'WAITING';
    case COMPLETED = 'COMPLETED';
    case EXITED = 'EXITED';
    case FAILED = 'FAILED';
    case CANCELLED = 'CANCELLED';
}