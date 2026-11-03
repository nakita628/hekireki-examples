<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Highlight extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Highlight';

    protected $fillable = [
        'color',
        'comment',
        'linkId',
        'userId',
        'startOffset',
        'endOffset',
        'text',
    ];

    protected $casts = [
        'linkId' => 'integer',
        'userId' => 'integer',
        'startOffset' => 'integer',
        'endOffset' => 'integer',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}