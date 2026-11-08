<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Submission extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Submission';

    protected $fillable = [
        'code',
        'isSuccessful',
        'userId',
        'challengeId',
    ];

    protected $casts = [
        'isSuccessful' => 'boolean',
        'challengeId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function challenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'challengeId');
    }

    public function vote(): HasMany
    {
        return $this->hasMany(Vote::class, 'submissionId');
    }
}