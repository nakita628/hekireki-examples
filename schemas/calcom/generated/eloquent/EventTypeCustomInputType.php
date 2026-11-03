<?php

namespace App\Models;

enum EventTypeCustomInputType: string
{
    case TEXT = 'text';
    case TEXTLONG = 'textLong';
    case NUMBER = 'number';
    case BOOL = 'bool';
    case RADIO = 'radio';
    case PHONE = 'phone';
}