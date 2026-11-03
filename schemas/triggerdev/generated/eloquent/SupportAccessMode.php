<?php

namespace App\Models;

enum SupportAccessMode: string
{
    case ALLOW = 'ALLOW';
    case REQUIRES_REQUEST = 'REQUIRES_REQUEST';
}