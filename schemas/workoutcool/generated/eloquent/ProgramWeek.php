<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProgramWeek extends Model
{
    protected $table = 'program_weeks';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'programId',
        'weekNumber',
        'title',
        'titleEn',
        'titleEs',
        'titlePt',
        'titleRu',
        'titleZhCn',
        'description',
        'descriptionEn',
        'descriptionEs',
        'descriptionPt',
        'descriptionRu',
        'descriptionZhCn',
    ];

    protected $casts = [
        'weekNumber' => 'integer',
    ];

    public function program(): BelongsTo
    {
        return $this->belongsTo(Program::class, 'programId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(ProgramSession::class, 'weekId');
    }
}