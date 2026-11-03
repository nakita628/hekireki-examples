<?php

namespace App\Models;

enum QueuedEmailStatus: string
{
    case pending = 'pending';
    case sent = 'sent';
    case skipped = 'skipped';
    case failed = 'failed';
}