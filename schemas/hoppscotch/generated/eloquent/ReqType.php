<?php

namespace App\Models;

enum ReqType: string
{
    case REST = 'REST';
    case GQL = 'GQL';
}