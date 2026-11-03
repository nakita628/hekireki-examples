<?php

namespace App\Models;

enum SpaceDataDbBindingState: string
{
    case ready = 'ready';
    case validating = 'validating';
    case initializing = 'initializing';
    case migrating = 'migrating';
    case error = 'error';
    case disabled = 'disabled';
}