<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DataroomDocument extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataroomDocument';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'dataroomId',
        'documentId',
        'folderId',
        'orderIndex',
        'hierarchicalIndex',
        'vectorStoreFileId',
    ];

    protected $casts = [
        'orderIndex' => 'integer',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function folder(): BelongsTo
    {
        return $this->belongsTo(DataroomFolder::class, 'folderId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'dataroomDocumentId');
    }

    public function questions(): HasMany
    {
        return $this->hasMany(DataroomQuestion::class, 'dataroomDocumentId');
    }

    public function dataroomFAQs(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'dataroomDocumentId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'dataroomDocumentId');
    }
}