<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Credential extends Model
{
    use PrismaDates;

    protected $table = 'credentials';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'provider',
        'provider_account_id',
        'type',
        'secret',
        'scopes',
        'expires_at',
    ];

    protected $casts = [
        'type' => CredentialType::class,
        'expires_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function calendarConnections(): HasMany
    {
        return $this->hasMany(CalendarConnection::class, 'credential_id');
    }

    public function conferencingConnections(): HasMany
    {
        return $this->hasMany(ConferencingConnection::class, 'credential_id');
    }
}