<?php

namespace App\Models;

enum PollStatus: string
{
    case open = 'open';
    case closed = 'closed';
    case scheduled = 'scheduled';
    case canceled = 'canceled';
}