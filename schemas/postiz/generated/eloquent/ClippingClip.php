<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ClippingClip extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ClippingClip';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'clippingId',
        'title',
        'content',
        'start',
        'end',
        'trimStart',
        'status',
        'error',
        'mediaId',
        'path',
        'thumbnail',
        'draftedAt',
    ];

    protected $attributes = [
        'status' => 'pending',
    ];

    protected $casts = [
        'start' => 'float',
        'end' => 'float',
        'trimStart' => 'integer',
        'draftedAt' => 'datetime',
    ];

    public function clipping(): BelongsTo
    {
        return $this->belongsTo(Clipping::class, 'clippingId');
    }
}