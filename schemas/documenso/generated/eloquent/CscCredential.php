<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CscCredential extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CscCredential';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'providerId',
        'credentialId',
        'certCache',
        'signatureAlgorithm',
        'keyType',
        'digestAlgorithm',
        'keyLenBits',
        'signAlgoParams',
        'serviceTokenCiphertext',
        'serviceTokenExpiresAt',
        'recipientId',
    ];

    protected $casts = [
        'certCache' => AsBytes::class,
        'keyLenBits' => 'integer',
        'serviceTokenCiphertext' => AsBytes::class,
        'serviceTokenExpiresAt' => 'datetime',
        'recipientId' => 'integer',
    ];

    public function recipient(): BelongsTo
    {
        return $this->belongsTo(Recipient::class, 'recipientId');
    }
}