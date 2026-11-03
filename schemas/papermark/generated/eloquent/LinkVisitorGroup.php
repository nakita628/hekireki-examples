<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LinkVisitorGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'LinkVisitorGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'linkId',
        'visitorGroupId',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function visitorGroup(): BelongsTo
    {
        return $this->belongsTo(VisitorGroup::class, 'visitorGroupId');
    }
}