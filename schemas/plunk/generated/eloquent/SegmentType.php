<?php

namespace App\Models;

enum SegmentType: string
{
    case DYNAMIC = 'DYNAMIC';
    case STATIC = 'STATIC';
}