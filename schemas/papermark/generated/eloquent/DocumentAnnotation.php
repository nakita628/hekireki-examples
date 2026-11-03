<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DocumentAnnotation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentAnnotation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'content',
        'pages',
        'documentId',
        'teamId',
        'createdById',
        'isVisible',
    ];

    protected $attributes = [
        'isVisible' => true,
    ];

    protected $casts = [
        'content' => 'array',
        'isVisible' => 'boolean',
    ];

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function images(): HasMany
    {
        return $this->hasMany(AnnotationImage::class, 'annotationId');
    }
}