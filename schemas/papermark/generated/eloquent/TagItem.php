<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TagItem extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TagItem';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'tagId',
        'itemType',
        'linkId',
        'documentId',
        'dataroomId',
        'taggedBy',
    ];

    protected $casts = [
        'itemType' => TagType::class,
    ];

    public function tag(): BelongsTo
    {
        return $this->belongsTo(Tag::class, 'tagId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }
}