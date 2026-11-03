<?php

namespace App\Models;

enum AssignmentReasonEnum: string
{
    case REASSIGNED = 'REASSIGNED';
    case RR_REASSIGNED = 'RR_REASSIGNED';
    case REROUTED = 'REROUTED';
    case SALESFORCE_ASSIGNMENT = 'SALESFORCE_ASSIGNMENT';
}