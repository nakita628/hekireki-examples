<?php

namespace App\Models;

enum BackgroundJobTaskStatus: string
{
    case PENDING = 'PENDING';
    case COMPLETED = 'COMPLETED';
    case FAILED = 'FAILED';
}