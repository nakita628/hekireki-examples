<?php

namespace App\Models;

enum IssueType: string
{
    case DEROGATORY = 'DEROGATORY';
    case OTHER = 'OTHER';
    case UNCLEAR = 'UNCLEAR';
    case BULLYING = 'BULLYING';
    case SPAM = 'SPAM';
    case HATESPEECH = 'HATESPEECH';
    case THREAT = 'THREAT';
}