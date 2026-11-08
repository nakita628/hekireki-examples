<?php

namespace App\Models;

enum ExerciseAttributeNameEnum: string
{
    case TYPE = 'TYPE';
    case PRIMARY_MUSCLE = 'PRIMARY_MUSCLE';
    case SECONDARY_MUSCLE = 'SECONDARY_MUSCLE';
    case EQUIPMENT = 'EQUIPMENT';
    case MECHANICS_TYPE = 'MECHANICS_TYPE';
}