<?php

namespace App\Models;

enum DocumentDistributionMethod: string
{
    case EMAIL = 'EMAIL';
    case NONE = 'NONE';
}