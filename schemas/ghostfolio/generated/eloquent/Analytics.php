<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Analytics extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Analytics';

    protected $primaryKey = 'userId';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'activityCount',
        'country',
        'dataProviderGhostfolioDailyRequests',
        'lastRequestAt',
        'lastRequestClient',
    ];

    protected $attributes = [
        'activityCount' => 0,
        'dataProviderGhostfolioDailyRequests' => 0,
    ];

    protected $casts = [
        'activityCount' => 'integer',
        'dataProviderGhostfolioDailyRequests' => 'integer',
        'lastRequestAt' => 'datetime',
        'lastRequestClient' => RequestClient::class,
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastRequestAt', $this->attributes)) {
                $this->setAttribute('lastRequestAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}