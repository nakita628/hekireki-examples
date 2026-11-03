<?php

namespace App\Models;

enum Plan: string
{
    case FREE = 'FREE';
    case STARTER = 'STARTER';
    case PRO = 'PRO';
    case LIFETIME = 'LIFETIME';
    case OFFERED = 'OFFERED';
    case CUSTOM = 'CUSTOM';
    case UNLIMITED = 'UNLIMITED';
    case ENTERPRISE = 'ENTERPRISE';
}