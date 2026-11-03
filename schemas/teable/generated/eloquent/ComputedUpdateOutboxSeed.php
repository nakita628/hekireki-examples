<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ComputedUpdateOutboxSeed extends Model
{
    protected $table = 'computed_update_outbox_seed';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'task_id',
        'table_id',
        'record_id',
    ];

    public function task(): BelongsTo
    {
        return $this->belongsTo(ComputedUpdateOutbox::class, 'task_id');
    }
}