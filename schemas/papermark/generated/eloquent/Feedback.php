<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Feedback extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Feedback';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'linkId',
        'data',
    ];

    protected $casts = [
        'data' => 'array',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function responses(): HasMany
    {
        return $this->hasMany(FeedbackResponse::class, 'feedbackId');
    }
}