<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SelectedSlots extends Model
{
    use PrismaDates;

    protected $table = 'SelectedSlots';

    public $timestamps = false;

    protected $fillable = [
        'eventTypeId',
        'userId',
        'slotUtcStartDate',
        'slotUtcEndDate',
        'uid',
        'releaseAt',
        'isSeat',
    ];

    protected $attributes = [
        'isSeat' => false,
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'userId' => 'integer',
        'slotUtcStartDate' => 'datetime',
        'slotUtcEndDate' => 'datetime',
        'releaseAt' => 'datetime',
        'isSeat' => 'boolean',
    ];
}