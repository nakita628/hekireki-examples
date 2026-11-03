<?php

namespace App\Models;

enum BulkActionStatus: string
{
    case PENDING = 'PENDING';
    case COMPLETED = 'COMPLETED';
    case ABORTED = 'ABORTED';
}