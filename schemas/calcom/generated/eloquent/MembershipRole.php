<?php

namespace App\Models;

enum MembershipRole: string
{
    case MEMBER = 'MEMBER';
    case ADMIN = 'ADMIN';
    case OWNER = 'OWNER';
}