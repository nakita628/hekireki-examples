<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CscSession extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'CscSession';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'envelopeId',
        'signingTime',
        'itemsJson',
        'encryptedSad',
        'sadExpiresAt',
        'recipientId',
    ];

    protected $casts = [
        'signingTime' => 'datetime',
        'itemsJson' => 'array',
        'encryptedSad' => AsBytes::class,
        'sadExpiresAt' => 'datetime',
        'recipientId' => 'integer',
    ];

    public function recipient(): BelongsTo
    {
        return $this->belongsTo(Recipient::class, 'recipientId');
    }
}