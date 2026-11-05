<?php

namespace App\Models;

enum ProjectScope: string
{
    case all = 'all';
    case selected = 'selected';
}