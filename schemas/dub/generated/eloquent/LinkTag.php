<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LinkTag extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'LinkTag';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'linkId',
        'tagId',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function tag(): BelongsTo
    {
        return $this->belongsTo(Tag::class, 'tagId');
    }
}