<?php

namespace App\Models;

enum BookingAuditType: string
{
    case RECORD_CREATED = 'record_created';
    case RECORD_UPDATED = 'record_updated';
    case RECORD_DELETED = 'record_deleted';
}