<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AnswerV2 extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AnswerV2';

    protected $fillable = [
        'blockId',
        'content',
        'attachedFileUrls',
        'resultId',
    ];

    protected $casts = [
        'attachedFileUrls' => 'array',
    ];

    public function result(): BelongsTo
    {
        return $this->belongsTo(Result::class, 'resultId');
    }
}