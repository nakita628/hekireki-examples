<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasUuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WatchlistAudit extends Model
{
    use HasUuids;
    use PrismaDates;

    protected $table = 'WatchlistAudit';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'type',
        'value',
        'description',
        'action',
        'changedAt',
        'changedByUserId',
        'watchlistId',
    ];

    protected $attributes = [
        'action' => 'REPORT',
    ];

    protected $casts = [
        'type' => WatchlistType::class,
        'action' => WatchlistAction::class,
        'changedAt' => 'datetime',
        'changedByUserId' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('changedAt', $this->attributes)) {
                $this->setAttribute('changedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function watchlist(): BelongsTo
    {
        return $this->belongsTo(Watchlist::class, 'watchlistId');
    }
}