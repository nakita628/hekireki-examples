<?php

namespace App\Models;

enum OrganisationMemberInviteStatus: string
{
    case ACCEPTED = 'ACCEPTED';
    case PENDING = 'PENDING';
    case DECLINED = 'DECLINED';
}