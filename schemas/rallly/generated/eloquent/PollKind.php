<?php

namespace App\Models;

enum PollKind: string
{
    case date = 'date';
    case time = 'time';
}