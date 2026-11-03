<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AgreementResponse extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AgreementResponse';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'agreementId',
        'viewId',
        'linkId',
        'signerEmail',
        'signerName',
        'signingStatus',
        'signingExternalId',
        'signingEnvelopeId',
        'signingDocumentId',
        'signedAt',
        'completedAt',
        'signedFileKey',
        'signedFileName',
        'signedFileStorageType',
    ];

    protected $attributes = [
        'signingStatus' => 'PENDING',
    ];

    protected $casts = [
        'signingDocumentId' => 'integer',
        'signedAt' => 'datetime',
        'completedAt' => 'datetime',
        'signedFileStorageType' => DocumentStorageType::class,
    ];

    public function agreement(): BelongsTo
    {
        return $this->belongsTo(Agreement::class, 'agreementId');
    }

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }
}