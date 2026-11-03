<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Result extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Result';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'typebotId',
        'variables',
        'isCompleted',
        'hasStarted',
        'isArchived',
        'lastChatSessionId',
    ];

    protected $attributes = [
        'isArchived' => false,
    ];

    protected $casts = [
        'variables' => 'array',
        'isCompleted' => 'boolean',
        'hasStarted' => 'boolean',
        'isArchived' => 'boolean',
    ];

    public function typebot(): BelongsTo
    {
        return $this->belongsTo(Typebot::class, 'typebotId');
    }

    public function answers(): HasMany
    {
        return $this->hasMany(Answer::class, 'resultId');
    }

    public function answersV2(): HasMany
    {
        return $this->hasMany(AnswerV2::class, 'resultId');
    }

    public function logs(): HasMany
    {
        return $this->hasMany(Log::class, 'resultId');
    }

    public function edges(): HasMany
    {
        return $this->hasMany(VisitedEdge::class, 'resultId');
    }

    public function setVariableHistory(): HasMany
    {
        return $this->hasMany(SetVariableHistoryItem::class, 'resultId');
    }
}