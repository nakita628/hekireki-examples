<?php

namespace App\Models;

enum OidcSigningKeyStatus: string
{
    case STAGED = 'STAGED';
    case ACTIVE = 'ACTIVE';
    case RETIRED = 'RETIRED';
}