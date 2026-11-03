<?php

namespace App\Models;

enum SendStatus: string
{
    case NOT_SENT = 'NOT_SENT';
    case SENT = 'SENT';
}