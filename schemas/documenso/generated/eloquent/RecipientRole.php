<?php

namespace App\Models;

enum RecipientRole: string
{
    case CC = 'CC';
    case SIGNER = 'SIGNER';
    case VIEWER = 'VIEWER';
    case APPROVER = 'APPROVER';
    case ASSISTANT = 'ASSISTANT';
}