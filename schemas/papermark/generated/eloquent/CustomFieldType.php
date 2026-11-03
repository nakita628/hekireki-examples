<?php

namespace App\Models;

enum CustomFieldType: string
{
    case SHORT_TEXT = 'SHORT_TEXT';
    case LONG_TEXT = 'LONG_TEXT';
    case NUMBER = 'NUMBER';
    case PHONE_NUMBER = 'PHONE_NUMBER';
    case URL = 'URL';
    case CHECKBOX = 'CHECKBOX';
    case SELECT = 'SELECT';
    case MULTI_SELECT = 'MULTI_SELECT';
}