<?php

namespace App\Models;

enum SeatChangeType: string
{
    case ADDITION = 'ADDITION';
    case REMOVAL = 'REMOVAL';
}