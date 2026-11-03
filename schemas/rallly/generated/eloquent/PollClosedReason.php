<?php

namespace App\Models;

enum PollClosedReason: string
{
    case auto = 'auto';
    case manual = 'manual';
}