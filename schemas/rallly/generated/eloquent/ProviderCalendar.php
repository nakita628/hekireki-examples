<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProviderCalendar extends Model
{
    use PrismaDates;

    protected $table = 'provider_calendars';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'calendar_connection_id',
        'provider_calendar_id',
        'name',
        'primary',
        'time_zone',
        'selected',
        'deleted',
        'writable',
        'provider_data',
        'last_synced_at',
    ];

    protected $attributes = [
        'primary' => false,
        'selected' => false,
        'deleted' => false,
        'writable' => false,
    ];

    protected $casts = [
        'primary' => 'boolean',
        'selected' => 'boolean',
        'deleted' => 'boolean',
        'writable' => 'boolean',
        'provider_data' => 'array',
        'last_synced_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('last_synced_at', $this->attributes)) {
                $this->setAttribute('last_synced_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function calendarConnection(): BelongsTo
    {
        return $this->belongsTo(CalendarConnection::class, 'calendar_connection_id');
    }

    public function userDefaultDestination(): HasMany
    {
        return $this->hasMany(User::class, 'default_destination_calendar_id');
    }
}