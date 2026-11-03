<?php

namespace App\Models;

enum SigningStatus: string
{
    case NOT_SIGNED = 'NOT_SIGNED';
    case SIGNED = 'SIGNED';
    case REJECTED = 'REJECTED';
}