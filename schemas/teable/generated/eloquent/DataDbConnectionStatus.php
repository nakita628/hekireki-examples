<?php

namespace App\Models;

enum DataDbConnectionStatus: string
{
    case pending = 'pending';
    case validating = 'validating';
    case ready = 'ready';
    case error = 'error';
    case migrating = 'migrating';
    case disabled = 'disabled';
}