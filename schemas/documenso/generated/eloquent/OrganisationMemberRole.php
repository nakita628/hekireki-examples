<?php

namespace App\Models;

enum OrganisationMemberRole: string
{
    case ADMIN = 'ADMIN';
    case MANAGER = 'MANAGER';
    case MEMBER = 'MEMBER';
}