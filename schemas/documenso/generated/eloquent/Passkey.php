<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Passkey extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Passkey';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'name',
        'lastUsedAt',
        'credentialId',
        'credentialPublicKey',
        'counter',
        'credentialDeviceType',
        'credentialBackedUp',
        'transports',
    ];

    protected $casts = [
        'userId' => 'integer',
        'lastUsedAt' => 'datetime',
        'credentialId' => AsBytes::class,
        'credentialPublicKey' => AsBytes::class,
        'counter' => 'integer',
        'credentialBackedUp' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}