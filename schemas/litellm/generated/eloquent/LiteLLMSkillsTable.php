<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMSkillsTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_SkillsTable';

    protected $primaryKey = 'skill_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'display_title',
        'description',
        'instructions',
        'source',
        'latest_version',
        'file_content',
        'file_name',
        'file_type',
        'metadata',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'source' => 'custom',
        'metadata' => '{}',
    ];

    protected $casts = [
        'file_content' => AsBytes::class,
        'metadata' => 'array',
    ];
}