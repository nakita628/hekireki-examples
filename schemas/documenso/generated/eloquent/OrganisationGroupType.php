<?php

namespace App\Models;

enum OrganisationGroupType: string
{
    case INTERNAL_ORGANISATION = 'INTERNAL_ORGANISATION';
    case INTERNAL_TEAM = 'INTERNAL_TEAM';
    case CUSTOM = 'CUSTOM';
}