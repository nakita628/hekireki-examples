<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class CalendarConnection extends Model
{
    use PrismaDates;

    protected $table = 'calendar_connections';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'provider',
        'integration_id',
        'provider_account_id',
        'email',
        'display_name',
        'credential_id',
    ];

    public function credential(): BelongsTo
    {
        return $this->belongsTo(Credential::class, 'credential_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function providerCalendars(): HasMany
    {
        return $this->hasMany(ProviderCalendar::class, 'calendar_connection_id');
    }
}