<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class InsightEvent extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'insight_events';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'insightId',
        'eventKind',
        'changeFrom',
        'changeTo',
    ];

    protected $casts = [
        'changeFrom' => 'array',
        'changeTo' => 'array',
    ];

    public function insight(): BelongsTo
    {
        return $this->belongsTo(ProjectInsight::class, 'insightId');
    }
}