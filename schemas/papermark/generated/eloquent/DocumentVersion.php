<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DocumentVersion extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentVersion';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'versionNumber',
        'documentId',
        'file',
        'originalFile',
        'type',
        'contentType',
        'fileSize',
        'storageType',
        'numPages',
        'isPrimary',
        'isVertical',
        'fileId',
        'vectorStoreFileId',
        'hasPages',
        'length',
    ];

    protected $attributes = [
        'storageType' => 'VERCEL_BLOB',
        'isPrimary' => false,
        'isVertical' => false,
        'hasPages' => false,
    ];

    protected $casts = [
        'versionNumber' => 'integer',
        'fileSize' => 'integer',
        'storageType' => DocumentStorageType::class,
        'numPages' => 'integer',
        'isPrimary' => 'boolean',
        'isVertical' => 'boolean',
        'hasPages' => 'boolean',
        'length' => 'integer',
    ];

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function pages(): HasMany
    {
        return $this->hasMany(DocumentPage::class, 'versionId');
    }

    public function redactionJobs(): HasMany
    {
        return $this->hasMany(DocumentRedactionJob::class, 'documentVersionId');
    }
}