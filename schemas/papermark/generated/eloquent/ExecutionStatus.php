<?php

namespace App\Models;

enum ExecutionStatus: string
{
    case PENDING = 'PENDING';
    case IN_PROGRESS = 'IN_PROGRESS';
    case COMPLETED = 'COMPLETED';
    case FAILED = 'FAILED';
    case BLOCKED = 'BLOCKED';
}