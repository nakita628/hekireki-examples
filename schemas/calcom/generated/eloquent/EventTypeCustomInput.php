<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EventTypeCustomInput extends Model
{
    protected $table = 'EventTypeCustomInput';

    public $timestamps = false;

    protected $fillable = [
        'eventTypeId',
        'label',
        'type',
        'options',
        'required',
        'placeholder',
    ];

    protected $attributes = [
        'placeholder' => '',
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'type' => EventTypeCustomInputType::class,
        'options' => 'array',
        'required' => 'boolean',
    ];

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }
}