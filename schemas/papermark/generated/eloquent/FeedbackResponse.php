<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class FeedbackResponse extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'FeedbackResponse';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'feedbackId',
        'data',
        'viewId',
    ];

    protected $casts = [
        'data' => 'array',
    ];

    public function feedback(): BelongsTo
    {
        return $this->belongsTo(Feedback::class, 'feedbackId');
    }

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }
}