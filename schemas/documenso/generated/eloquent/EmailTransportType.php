<?php

namespace App\Models;

enum EmailTransportType: string
{
    case SMTP_AUTH = 'SMTP_AUTH';
    case SMTP_API = 'SMTP_API';
    case RESEND = 'RESEND';
    case MAILCHANNELS = 'MAILCHANNELS';
}