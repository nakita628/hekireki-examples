<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AnnotationImage extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AnnotationImage';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'filename',
        'url',
        'size',
        'mimeType',
        'annotationId',
    ];

    protected $casts = [
        'size' => 'integer',
    ];

    public function annotation(): BelongsTo
    {
        return $this->belongsTo(DocumentAnnotation::class, 'annotationId');
    }
}