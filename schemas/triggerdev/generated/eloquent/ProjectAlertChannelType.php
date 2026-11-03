<?php

namespace App\Models;

enum ProjectAlertChannelType: string
{
    case EMAIL = 'EMAIL';
    case SLACK = 'SLACK';
    case WEBHOOK = 'WEBHOOK';
}