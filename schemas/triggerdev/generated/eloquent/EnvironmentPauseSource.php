<?php

namespace App\Models;

enum EnvironmentPauseSource: string
{
    case BILLING_LIMIT = 'BILLING_LIMIT';
    case ADMIN_BAN = 'ADMIN_BAN';
}