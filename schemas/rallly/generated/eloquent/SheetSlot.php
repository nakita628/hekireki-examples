<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class SheetSlot extends Model
{
    use PrismaDates;

    protected $table = 'sheet_slots';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'sheet_id',
        'event_type_id',
        'start_time',
    ];

    protected $casts = [
        'start_time' => 'datetime',
    ];

    public function sheet(): BelongsTo
    {
        return $this->belongsTo(Sheet::class, 'sheet_id');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'event_type_id');
    }

    public function scheduledEvent(): HasOne
    {
        return $this->hasOne(ScheduledEvent::class, 'sheet_slot_id');
    }
}