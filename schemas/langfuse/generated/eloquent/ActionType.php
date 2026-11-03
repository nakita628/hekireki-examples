<?php

namespace App\Models;

enum ActionType: string
{
    case WEBHOOK = 'WEBHOOK';
    case SLACK = 'SLACK';
    case GITHUB_DISPATCH = 'GITHUB_DISPATCH';
    case PAGERDUTY = 'PAGERDUTY';
}