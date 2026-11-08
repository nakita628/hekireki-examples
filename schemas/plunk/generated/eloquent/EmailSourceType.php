<?php

namespace App\Models;

enum EmailSourceType: string
{
    case TRANSACTIONAL = 'TRANSACTIONAL';
    case CAMPAIGN = 'CAMPAIGN';
    case WORKFLOW = 'WORKFLOW';
    case INBOUND = 'INBOUND';
}