<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DocumentRedaction extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentRedaction';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'jobId',
        'pageNumber',
        'x',
        'y',
        'width',
        'height',
        'textPreview',
        'category',
        'confidence',
        'reason',
        'source',
        'status',
    ];

    protected $attributes = [
        'source' => 'AI',
        'status' => 'PENDING',
    ];

    protected $casts = [
        'pageNumber' => 'integer',
        'x' => 'float',
        'y' => 'float',
        'width' => 'float',
        'height' => 'float',
    ];

    public function job(): BelongsTo
    {
        return $this->belongsTo(DocumentRedactionJob::class, 'jobId');
    }
}