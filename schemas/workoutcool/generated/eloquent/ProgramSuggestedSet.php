<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ProgramSuggestedSet extends Model
{
    protected $table = 'program_suggested_sets';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'programSessionExerciseId',
        'setIndex',
        'types',
        'valuesInt',
        'valuesSec',
        'units',
    ];

    protected $casts = [
        'setIndex' => 'integer',
    ];

    public function programSessionExercise(): BelongsTo
    {
        return $this->belongsTo(ProgramSessionExercise::class, 'programSessionExerciseId');
    }
}