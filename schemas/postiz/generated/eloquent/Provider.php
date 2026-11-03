<?php

namespace App\Models;

enum Provider: string
{
    case LOCAL = 'LOCAL';
    case GITHUB = 'GITHUB';
    case GOOGLE = 'GOOGLE';
    case APPLE = 'APPLE';
    case FARCASTER = 'FARCASTER';
    case WALLET = 'WALLET';
    case GENERIC = 'GENERIC';
}