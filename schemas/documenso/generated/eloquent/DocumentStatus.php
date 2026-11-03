<?php

namespace App\Models;

enum DocumentStatus: string
{
    case DRAFT = 'DRAFT';
    case PENDING = 'PENDING';
    case COMPLETED = 'COMPLETED';
    case REJECTED = 'REJECTED';
    case CANCELLED = 'CANCELLED';
}