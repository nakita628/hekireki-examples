<?php

namespace App\Models;

enum ScheduledEventStatus: string
{
    case confirmed = 'confirmed';
    case canceled = 'canceled';
    case unconfirmed = 'unconfirmed';
}