<?php

namespace App\Models;

enum FieldType: string
{
    case SIGNATURE = 'SIGNATURE';
    case FREE_SIGNATURE = 'FREE_SIGNATURE';
    case INITIALS = 'INITIALS';
    case NAME = 'NAME';
    case EMAIL = 'EMAIL';
    case DATE = 'DATE';
    case TEXT = 'TEXT';
    case NUMBER = 'NUMBER';
    case RADIO = 'RADIO';
    case CHECKBOX = 'CHECKBOX';
    case DROPDOWN = 'DROPDOWN';
}