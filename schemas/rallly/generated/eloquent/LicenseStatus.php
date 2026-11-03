<?php

namespace App\Models;

enum LicenseStatus: string
{
    case ACTIVE = 'ACTIVE';
    case REVOKED = 'REVOKED';
}