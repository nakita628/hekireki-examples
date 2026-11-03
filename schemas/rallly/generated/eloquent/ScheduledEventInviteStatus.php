<?php

namespace App\Models;

enum ScheduledEventInviteStatus: string
{
    case pending = 'pending';
    case accepted = 'accepted';
    case declined = 'declined';
    case tentative = 'tentative';
}