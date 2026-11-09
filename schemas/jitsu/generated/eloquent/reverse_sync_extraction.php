<?php

namespace App\Models;

enum reverse_sync_extraction: string
{
    case cursor = 'cursor';
    case full = 'full';
}