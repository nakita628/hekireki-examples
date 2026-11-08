<?php

namespace App\Models;

enum WorkflowStepType: string
{
    case TRIGGER = 'TRIGGER';
    case SEND_EMAIL = 'SEND_EMAIL';
    case DELAY = 'DELAY';
    case WAIT_FOR_EVENT = 'WAIT_FOR_EVENT';
    case CONDITION = 'CONDITION';
    case EXIT = 'EXIT';
    case WEBHOOK = 'WEBHOOK';
    case UPDATE_CONTACT = 'UPDATE_CONTACT';
}