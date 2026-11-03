<?php

namespace App\Models;

enum LicenseType: string
{
    case PLUS = 'PLUS';
    case ORGANIZATION = 'ORGANIZATION';
    case ENTERPRISE = 'ENTERPRISE';
}