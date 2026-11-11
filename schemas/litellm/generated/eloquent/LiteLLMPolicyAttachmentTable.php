<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMPolicyAttachmentTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_PolicyAttachmentTable';

    protected $primaryKey = 'attachment_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'policy_name',
        'scope',
        'teams',
        'keys',
        'models',
        'tags',
        'priority',
        'is_default',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'is_default' => false,
    ];

    protected $casts = [
        'priority' => 'integer',
        'is_default' => 'boolean',
    ];
}