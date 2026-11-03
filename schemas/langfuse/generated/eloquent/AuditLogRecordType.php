<?php

namespace App\Models;

enum AuditLogRecordType: string
{
    case USER = 'USER';
    case API_KEY = 'API_KEY';
}