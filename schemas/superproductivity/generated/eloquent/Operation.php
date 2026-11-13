<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Operation extends Model
{
    protected $table = 'operations';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'client_id',
        'server_seq',
        'action_type',
        'op_type',
        'entity_type',
        'entity_id',
        'entity_ids',
        'payload',
        'payload_bytes',
        'vector_clock',
        'schema_version',
        'client_timestamp',
        'received_at',
        'is_payload_encrypted',
        'sync_import_reason',
        'repair_base_server_seq',
    ];

    protected $attributes = [
        'payload_bytes' => 0,
        'is_payload_encrypted' => false,
    ];

    protected $casts = [
        'user_id' => 'integer',
        'server_seq' => 'integer',
        'payload' => 'array',
        'payload_bytes' => 'integer',
        'vector_clock' => 'array',
        'schema_version' => 'integer',
        'client_timestamp' => 'integer',
        'received_at' => 'integer',
        'is_payload_encrypted' => 'boolean',
        'repair_base_server_seq' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}