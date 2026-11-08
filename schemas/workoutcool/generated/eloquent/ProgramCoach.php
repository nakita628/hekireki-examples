<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ProgramCoach extends Model
{
    protected $table = 'program_coaches';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'programId',
        'name',
        'image',
        'order',
    ];

    protected $attributes = [
        'order' => 0,
    ];

    protected $casts = [
        'order' => 'integer',
    ];

    public function program(): BelongsTo
    {
        return $this->belongsTo(Program::class, 'programId');
    }
}