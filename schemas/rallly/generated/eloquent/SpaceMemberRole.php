<?php

namespace App\Models;

enum SpaceMemberRole: string
{
    case ADMIN = 'ADMIN';
    case MEMBER = 'MEMBER';
}