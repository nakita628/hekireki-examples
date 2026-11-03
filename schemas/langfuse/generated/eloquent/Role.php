<?php

namespace App\Models;

enum Role: string
{
    case OWNER = 'OWNER';
    case ADMIN = 'ADMIN';
    case MEMBER = 'MEMBER';
    case VIEWER = 'VIEWER';
    case NONE = 'NONE';
    case AI_GATEWAY = 'AI_GATEWAY';
}