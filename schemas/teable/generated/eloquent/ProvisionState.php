<?php

namespace App\Models;

enum ProvisionState: string
{
    case pending = 'pending';
    case ready = 'ready';
    case error = 'error';
    case deleting = 'deleting';
}