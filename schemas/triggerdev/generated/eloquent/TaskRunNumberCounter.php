<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskRunNumberCounter extends Model
{
    protected $table = 'TaskRunNumberCounter';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'taskIdentifier',
        'environmentId',
        'lastNumber',
    ];

    protected $attributes = [
        'lastNumber' => 0,
    ];

    protected $casts = [
        'lastNumber' => 'integer',
    ];

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}