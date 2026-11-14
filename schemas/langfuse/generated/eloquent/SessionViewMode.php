<?php

namespace App\Models;

enum SessionViewMode: string
{
    case conversational = 'conversational';
    case list = 'list';
}