<?php

namespace App\Models;

enum NotificationType: string
{
    case LIKE = 'LIKE';
    case REPLY = 'REPLY';
    case MENTION = 'MENTION';
    case MISC = 'MISC';
    case SYSTEM = 'SYSTEM';
}