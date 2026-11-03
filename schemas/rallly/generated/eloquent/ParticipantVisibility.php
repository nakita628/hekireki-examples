<?php

namespace App\Models;

enum ParticipantVisibility: string
{
    case full = 'full';
    case scoresOnly = 'scoresOnly';
    case limited = 'limited';
}