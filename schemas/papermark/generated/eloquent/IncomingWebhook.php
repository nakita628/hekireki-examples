<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class IncomingWebhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'IncomingWebhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'externalId',
        'name',
        'secret',
        'source',
        'actions',
        'consecutiveFailures',
        'lastFailedAt',
        'disabledAt',
        'teamId',
    ];

    protected $attributes = [
        'consecutiveFailures' => 0,
    ];

    protected $casts = [
        'consecutiveFailures' => 'integer',
        'lastFailedAt' => 'datetime',
        'disabledAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}