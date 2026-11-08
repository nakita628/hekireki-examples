<?php

namespace App\Models;

enum VoteType: string
{
    case CHALLENGE = 'CHALLENGE';
    case COMMENT = 'COMMENT';
    case SUBMISSION = 'SUBMISSION';
    case SHAREDSOLUTION = 'SHAREDSOLUTION';
}