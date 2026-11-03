<?php

namespace App\Models;

enum SpaceDataDbMigrationJobState: string
{
    case pending = 'pending';
    case waiting_worker = 'waiting_worker';
    case preflight = 'preflight';
    case freezing_writes = 'freezing_writes';
    case copying = 'copying';
    case validating = 'validating';
    case switching = 'switching';
    case succeeded = 'succeeded';
    case failed = 'failed';
    case canceled = 'canceled';
    case rolled_back = 'rolled_back';
}