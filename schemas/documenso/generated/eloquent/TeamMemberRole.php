<?php

namespace App\Models;

enum TeamMemberRole: string
{
    case ADMIN = 'ADMIN';
    case MANAGER = 'MANAGER';
    case MEMBER = 'MEMBER';
}