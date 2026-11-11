<?php

namespace App\Models;

enum TrustedDeployScope: string
{
    case ANY = 'ANY';
    case BRANCH = 'BRANCH';
    case TAG = 'TAG';
    case ENVIRONMENT = 'ENVIRONMENT';
}