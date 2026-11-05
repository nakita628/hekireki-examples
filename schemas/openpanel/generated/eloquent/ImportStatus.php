<?php

namespace App\Models;

enum ImportStatus: string
{
    case pending = 'pending';
    case processing = 'processing';
    case completed = 'completed';
    case failed = 'failed';
}