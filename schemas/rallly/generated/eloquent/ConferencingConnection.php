<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ConferencingConnection extends Model
{
    use PrismaDates;

    protected $table = 'conferencing_connections';

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
}