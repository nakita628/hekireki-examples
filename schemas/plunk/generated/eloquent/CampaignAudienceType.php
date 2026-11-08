<?php

namespace App\Models;

enum CampaignAudienceType: string
{
    case ALL = 'ALL';
    case FILTERED = 'FILTERED';
    case SEGMENT = 'SEGMENT';
}