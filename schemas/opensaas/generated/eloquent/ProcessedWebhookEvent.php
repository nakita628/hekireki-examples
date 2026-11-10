<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class ProcessedWebhookEvent extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ProcessedWebhookEvent';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'paymentProcessorEventId',
        'eventType',
    ];
}