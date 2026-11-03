<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DocumentUpload extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentUpload';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'documentId',
        'teamId',
        'viewerId',
        'viewId',
        'linkId',
        'dataroomId',
        'dataroomDocumentId',
        'taskId',
        'originalFilename',
        'fileSize',
        'numPages',
        'mimeType',
        'uploadedAt',
    ];

    protected $casts = [
        'fileSize' => 'integer',
        'numPages' => 'integer',
        'uploadedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('uploadedAt', $this->attributes)) {
                $this->setAttribute('uploadedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function dataroomDocument(): BelongsTo
    {
        return $this->belongsTo(DataroomDocument::class, 'dataroomDocumentId');
    }

    public function task(): BelongsTo
    {
        return $this->belongsTo(Task::class, 'taskId');
    }
}