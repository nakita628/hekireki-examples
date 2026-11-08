<?php

namespace App\Models;

enum StorageIntegrationAuthType: string
{
    case ACCESS_KEYS = 'ACCESS_KEYS';
    case ROLE_ARN = 'ROLE_ARN';
}