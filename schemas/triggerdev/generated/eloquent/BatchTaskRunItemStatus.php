<?php

namespace App\Models;

enum BatchTaskRunItemStatus: string
{
    case PENDING = 'PENDING';
    case FAILED = 'FAILED';
    case CANCELED = 'CANCELED';
    case COMPLETED = 'COMPLETED';
}