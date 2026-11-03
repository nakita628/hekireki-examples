<?php

namespace App\Models;

enum AttributeType: string
{
    case TEXT = 'TEXT';
    case NUMBER = 'NUMBER';
    case SINGLE_SELECT = 'SINGLE_SELECT';
    case MULTI_SELECT = 'MULTI_SELECT';
}