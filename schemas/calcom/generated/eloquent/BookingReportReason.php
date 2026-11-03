<?php

namespace App\Models;

enum BookingReportReason: string
{
    case SPAM = 'SPAM';
    case DONT_KNOW_PERSON = 'dont_know_person';
    case OTHER = 'OTHER';
}