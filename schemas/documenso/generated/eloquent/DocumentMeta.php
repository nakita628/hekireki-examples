<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;

class DocumentMeta extends Model
{
    protected $table = 'DocumentMeta';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'subject',
        'message',
        'timezone',
        'dateFormat',
        'redirectUrl',
        'signingOrder',
        'allowDictateNextSigner',
        'typedSignatureEnabled',
        'uploadSignatureEnabled',
        'drawSignatureEnabled',
        'language',
        'distributionMethod',
        'emailSettings',
        'emailReplyTo',
        'emailId',
        'envelopeExpirationPeriod',
        'reminderSettings',
    ];

    protected $attributes = [
        'timezone' => 'Etc/UTC',
        'dateFormat' => 'yyyy-MM-dd hh:mm a',
        'signingOrder' => 'PARALLEL',
        'allowDictateNextSigner' => false,
        'typedSignatureEnabled' => true,
        'uploadSignatureEnabled' => true,
        'drawSignatureEnabled' => true,
        'language' => 'en',
        'distributionMethod' => 'EMAIL',
    ];

    protected $casts = [
        'signingOrder' => DocumentSigningOrder::class,
        'allowDictateNextSigner' => 'boolean',
        'typedSignatureEnabled' => 'boolean',
        'uploadSignatureEnabled' => 'boolean',
        'drawSignatureEnabled' => 'boolean',
        'distributionMethod' => DocumentDistributionMethod::class,
        'emailSettings' => 'array',
        'envelopeExpirationPeriod' => 'array',
        'reminderSettings' => 'array',
    ];

    public function envelope(): HasOne
    {
        return $this->hasOne(Envelope::class, 'documentMetaId');
    }
}