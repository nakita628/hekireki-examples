<?php

namespace App\Models;

enum NotificationType: string
{
    case COMMENT_MENTION = 'COMMENT_MENTION';
}