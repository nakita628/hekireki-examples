<?php

namespace App\Models;

enum OrgMemberRole: string
{
    case ADMIN = 'ADMIN';
    case MEMBER = 'MEMBER';
}