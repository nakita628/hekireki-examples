<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Document extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Document';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'file',
        'originalFile',
        'type',
        'contentType',
        'storageType',
        'numPages',
        'teamId',
        'ownerId',
        'assistantEnabled',
        'advancedExcelEnabled',
        'agentsEnabled',
        'downloadOnly',
        'hiddenInAllDocuments',
        'folderId',
        'isExternalUpload',
    ];

    protected $attributes = [
        'storageType' => 'VERCEL_BLOB',
        'assistantEnabled' => false,
        'advancedExcelEnabled' => false,
        'agentsEnabled' => false,
        'downloadOnly' => false,
        'hiddenInAllDocuments' => false,
        'isExternalUpload' => false,
    ];

    protected $casts = [
        'storageType' => DocumentStorageType::class,
        'numPages' => 'integer',
        'assistantEnabled' => 'boolean',
        'advancedExcelEnabled' => 'boolean',
        'agentsEnabled' => 'boolean',
        'downloadOnly' => 'boolean',
        'hiddenInAllDocuments' => 'boolean',
        'isExternalUpload' => 'boolean',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'ownerId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'documentId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'documentId');
    }

    public function versions(): HasMany
    {
        return $this->hasMany(DocumentVersion::class, 'documentId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'documentId');
    }

    public function datarooms(): HasMany
    {
        return $this->hasMany(DataroomDocument::class, 'documentId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(TagItem::class, 'documentId');
    }

    public function annotations(): HasMany
    {
        return $this->hasMany(DocumentAnnotation::class, 'documentId');
    }

    public function redactionJobs(): HasMany
    {
        return $this->hasMany(DocumentRedactionJob::class, 'documentId');
    }

    public function uploadedDocument(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'documentId');
    }
}