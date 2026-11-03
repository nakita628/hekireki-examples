<?php

namespace App\Models;

enum BulkActionNotificationType: string
{
    case NONE = 'NONE';
    case EMAIL = 'EMAIL';
}