<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMToolTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ToolTable';

    protected $primaryKey = 'tool_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'tool_name',
        'origin',
        'input_policy',
        'output_policy',
        'call_count',
        'assignments',
        'key_hash',
        'team_id',
        'key_alias',
        'user_agent',
        'last_used_at',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'input_policy' => 'untrusted',
        'output_policy' => 'untrusted',
        'call_count' => 0,
        'assignments' => '{}',
    ];

    protected $casts = [
        'call_count' => 'integer',
        'assignments' => 'array',
        'last_used_at' => 'datetime',
    ];
}