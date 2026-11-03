<?php

namespace App\Models;

enum EmailDomainStatus: string
{
    case PENDING = 'PENDING';
    case ACTIVE = 'ACTIVE';
}