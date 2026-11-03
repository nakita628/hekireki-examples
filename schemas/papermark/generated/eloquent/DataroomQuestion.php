<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DataroomQuestion extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataroomQuestion';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'content',
        'category',
        'priority',
        'status',
        'orderIndex',
        'importBatchId',
        'dataroomId',
        'teamId',
        'createdByUserId',
        'dataroomDocumentId',
        'documentPageNumber',
    ];

    protected $attributes = [
        'status' => 'unanswered',
    ];

    protected $casts = [
        'orderIndex' => 'integer',
        'documentPageNumber' => 'integer',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdByUserId');
    }

    public function dataroomDocument(): BelongsTo
    {
        return $this->belongsTo(DataroomDocument::class, 'dataroomDocumentId');
    }

    public function assignments(): HasMany
    {
        return $this->hasMany(DataroomQuestionAssignment::class, 'questionId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'questionId');
    }
}