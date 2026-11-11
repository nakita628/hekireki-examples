<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMSkillSubmission extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_SkillSubmission';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'plugin_name',
        'operation',
        'status',
        'proposal_json',
        'archive_content',
        'submitted_by',
        'base_plugin_id',
        'base_updated_at',
        'reviewed_by',
        'reviewed_at',
        'rejection_reason',
        'scan_json',
    ];

    protected $attributes = [
        'status' => 'pending_review',
    ];

    protected $casts = [
        'archive_content' => AsBytes::class,
        'base_updated_at' => 'datetime',
        'reviewed_at' => 'datetime',
    ];
}