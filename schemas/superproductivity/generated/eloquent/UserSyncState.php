<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserSyncState extends Model
{
    protected $table = 'user_sync_state';

    protected $primaryKey = 'user_id';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'last_seq',
        'last_snapshot_seq',
        'snapshot_data',
        'snapshot_at',
        'snapshot_schema_version',
        'latest_full_state_seq',
        'latest_full_state_vector_clock',
        'latest_state_replacement_seq',
    ];

    protected $attributes = [
        'last_seq' => 0,
        'snapshot_schema_version' => 1,
    ];

    protected $casts = [
        'last_seq' => 'integer',
        'last_snapshot_seq' => 'integer',
        'snapshot_data' => AsBytes::class,
        'snapshot_at' => 'integer',
        'snapshot_schema_version' => 'integer',
        'latest_full_state_seq' => 'integer',
        'latest_full_state_vector_clock' => 'array',
        'latest_state_replacement_seq' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}