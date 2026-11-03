<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class PendingDocumentUpload extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'PendingDocumentUpload';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'uploadId',
        'teamId',
        'tokenId',
        'key',
        'contentType',
        'storageType',
        'expiresAt',
        'consumedAt',
    ];

    protected $attributes = [
        'storageType' => 'S3_PATH',
    ];

    protected $casts = [
        'storageType' => DocumentStorageType::class,
        'expiresAt' => 'datetime',
        'consumedAt' => 'datetime',
    ];
}