<?php

namespace App\Models;

enum CancellationReasonRequirement: string
{
    case MANDATORY_BOTH = 'MANDATORY_BOTH';
    case MANDATORY_HOST_ONLY = 'MANDATORY_HOST_ONLY';
    case MANDATORY_ATTENDEE_ONLY = 'MANDATORY_ATTENDEE_ONLY';
    case OPTIONAL_BOTH = 'OPTIONAL_BOTH';
}