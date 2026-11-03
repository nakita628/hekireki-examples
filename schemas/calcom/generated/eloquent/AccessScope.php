<?php

namespace App\Models;

enum AccessScope: string
{
    case READ_BOOKING = 'READ_BOOKING';
    case READ_PROFILE = 'READ_PROFILE';
}