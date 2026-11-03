<?php

namespace App\Models;

enum LinkAudienceType: string
{
    case GENERAL = 'GENERAL';
    case GROUP = 'GROUP';
    case TEAM = 'TEAM';
}