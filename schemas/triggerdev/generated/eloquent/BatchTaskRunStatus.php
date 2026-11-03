<?php

namespace App\Models;

enum BatchTaskRunStatus: string
{
    case PENDING = 'PENDING';
    case PROCESSING = 'PROCESSING';
    case COMPLETED = 'COMPLETED';
    case PARTIAL_FAILED = 'PARTIAL_FAILED';
    case ABORTED = 'ABORTED';
}