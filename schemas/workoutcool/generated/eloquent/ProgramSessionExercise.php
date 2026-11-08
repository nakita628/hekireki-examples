<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProgramSessionExercise extends Model
{
    protected $table = 'program_session_exercises';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'sessionId',
        'exerciseId',
        'order',
        'instructions',
        'instructionsEn',
        'instructionsEs',
        'instructionsPt',
        'instructionsRu',
        'instructionsZhCn',
    ];

    protected $casts = [
        'order' => 'integer',
    ];

    public function session(): BelongsTo
    {
        return $this->belongsTo(ProgramSession::class, 'sessionId');
    }

    public function exercise(): BelongsTo
    {
        return $this->belongsTo(Exercise::class, 'exerciseId');
    }

    public function suggestedSets(): HasMany
    {
        return $this->hasMany(ProgramSuggestedSet::class, 'programSessionExerciseId');
    }
}