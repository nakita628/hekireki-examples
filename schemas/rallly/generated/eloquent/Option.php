<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Option extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'options';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'start_time',
        'duration_minutes',
        'poll_id',
    ];

    protected $attributes = [
        'duration_minutes' => 0,
    ];

    protected $casts = [
        'start_time' => 'datetime',
        'duration_minutes' => 'integer',
    ];

    public function poll(): BelongsTo
    {
        return $this->belongsTo(Poll::class, 'poll_id');
    }

    public function votes(): HasMany
    {
        return $this->hasMany(Vote::class, 'option_id');
    }
}