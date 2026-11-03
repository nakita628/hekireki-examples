<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasUuids;
use Illuminate\Database\Eloquent\Model;

class WatchlistEventAudit extends Model
{
    use HasUuids;
    use PrismaDates;

    protected $table = 'WatchlistEventAudit';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'watchlistId',
        'eventTypeId',
        'actionTaken',
        'timestamp',
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'actionTaken' => WatchlistAction::class,
        'timestamp' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('timestamp', $this->attributes)) {
                $this->setAttribute('timestamp', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}