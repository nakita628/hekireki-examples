<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Watchlist extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'Watchlist';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'type',
        'value',
        'description',
        'isGlobal',
        'organizationId',
        'action',
        'source',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'isGlobal' => false,
        'action' => 'REPORT',
        'source' => 'MANUAL',
    ];

    protected $casts = [
        'type' => WatchlistType::class,
        'isGlobal' => 'boolean',
        'organizationId' => 'integer',
        'action' => WatchlistAction::class,
        'source' => WatchlistSource::class,
        'lastUpdatedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function bookingReports(): HasMany
    {
        return $this->hasMany(BookingReport::class, 'watchlistId');
    }

    public function globalBookingReports(): HasMany
    {
        return $this->hasMany(BookingReport::class, 'globalWatchlistId');
    }

    public function audits(): HasMany
    {
        return $this->hasMany(WatchlistAudit::class, 'watchlistId');
    }
}