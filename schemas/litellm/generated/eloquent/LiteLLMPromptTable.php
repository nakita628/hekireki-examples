<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMPromptTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_PromptTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'prompt_id',
        'version',
        'environment',
        'created_by',
        'litellm_params',
        'prompt_info',
    ];

    protected $attributes = [
        'version' => 1,
        'environment' => 'development',
    ];

    protected $casts = [
        'version' => 'integer',
        'litellm_params' => 'array',
        'prompt_info' => 'array',
    ];
}