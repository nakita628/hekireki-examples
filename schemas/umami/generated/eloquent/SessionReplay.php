<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SessionReplay extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['started_at', 'ended_at', 'created_at'];

    protected $table = 'session_replay';

    protected $primaryKey = 'replay_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'replay_id',
        'website_id',
        'session_id',
        'visit_id',
        'chunk_index',
        'events',
        'event_count',
        'started_at',
        'ended_at',
    ];

    protected $casts = [
        'chunk_index' => 'integer',
        'events' => AsBytes::class,
        'event_count' => 'integer',
        'started_at' => 'datetime',
        'ended_at' => 'datetime',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}