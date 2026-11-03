<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class HashedLink extends Model
{
    use PrismaDates;

    protected $table = 'HashedLink';

    public $timestamps = false;

    protected $fillable = [
        'link',
        'eventTypeId',
        'expiresAt',
        'maxUsageCount',
        'usageCount',
    ];

    protected $attributes = [
        'maxUsageCount' => 1,
        'usageCount' => 0,
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'expiresAt' => 'datetime',
        'maxUsageCount' => 'integer',
        'usageCount' => 'integer',
    ];

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }
}