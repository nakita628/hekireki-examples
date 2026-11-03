<?php

namespace App\Models;

enum AppMigrationStatus: string
{
    case APPLIED = 'APPLIED';
    case PENDING = 'PENDING';
    case FAILED = 'FAILED';
}