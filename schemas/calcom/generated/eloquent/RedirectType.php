<?php

namespace App\Models;

enum RedirectType: string
{
    case UserEventType = 'user-event-type';
    case TeamEventType = 'team-event-type';
    case User = 'user';
    case Team = 'team';
}