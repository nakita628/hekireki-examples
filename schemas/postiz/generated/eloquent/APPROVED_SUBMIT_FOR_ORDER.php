<?php

namespace App\Models;

enum APPROVED_SUBMIT_FOR_ORDER: string
{
    case NO = 'NO';
    case WAITING_CONFIRMATION = 'WAITING_CONFIRMATION';
    case YES = 'YES';
}