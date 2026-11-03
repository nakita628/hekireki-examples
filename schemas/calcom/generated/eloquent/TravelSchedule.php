<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TravelSchedule extends Model
{
    use PrismaDates;

    protected $table = 'TravelSchedule';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'timeZone',
        'startDate',
        'endDate',
        'prevTimeZone',
    ];

    protected $casts = [
        'userId' => 'integer',
        'startDate' => 'datetime',
        'endDate' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}