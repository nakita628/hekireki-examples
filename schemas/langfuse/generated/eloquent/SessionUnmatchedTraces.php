<?php

namespace App\Models;

enum SessionUnmatchedTraces: string
{
    case show = 'show';
    case hide = 'hide';
}