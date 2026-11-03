<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EventType extends Model
{
    use PrismaDates;

    protected $table = 'event_types';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'host_id',
        'name',
        'duration_minutes',
        'capacity',
        'description',
        'location',
        'deleted',
        'deleted_at',
    ];

    protected $attributes = [
        'deleted' => false,
    ];

    protected $casts = [
        'duration_minutes' => 'integer',
        'capacity' => 'integer',
        'location' => 'array',
        'deleted' => 'boolean',
        'deleted_at' => 'datetime',
    ];

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function host(): BelongsTo
    {
        return $this->belongsTo(User::class, 'host_id');
    }

    public function slots(): HasMany
    {
        return $this->hasMany(SheetSlot::class, 'event_type_id');
    }

    public function scheduledEvents(): HasMany
    {
        return $this->hasMany(ScheduledEvent::class, 'event_type_id');
    }
}