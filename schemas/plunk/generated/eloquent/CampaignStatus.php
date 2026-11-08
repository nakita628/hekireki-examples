<?php

namespace App\Models;

enum CampaignStatus: string
{
    case DRAFT = 'DRAFT';
    case SCHEDULED = 'SCHEDULED';
    case SENDING = 'SENDING';
    case SENT = 'SENT';
    case CANCELLED = 'CANCELLED';
}