<?php

namespace App\Models;

enum BaseDataDbMoveJobState: string
{
    case pending = 'pending';
    case waiting_worker = 'waiting_worker';
    case copying_base_schema = 'copying_base_schema';
    case copying_shared_rows = 'copying_shared_rows';
    case validating = 'validating';
    case switching = 'switching';
    case succeeded = 'succeeded';
    case failed = 'failed';
    case cancelled = 'cancelled';
}