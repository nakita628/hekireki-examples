<?php

namespace App\Models;

enum AuditActorType: string
{
    case USER = 'user';
    case GUEST = 'guest';
    case ATTENDEE = 'attendee';
    case SYSTEM = 'system';
    case APP = 'app';
}