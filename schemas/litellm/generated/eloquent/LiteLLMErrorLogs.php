<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMErrorLogs extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ErrorLogs';

    protected $primaryKey = 'request_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'startTime',
        'endTime',
        'api_base',
        'model_group',
        'litellm_model_name',
        'model_id',
        'request_kwargs',
        'exception_type',
        'exception_string',
        'status_code',
    ];

    protected $attributes = [
        'api_base' => '',
        'model_group' => '',
        'litellm_model_name' => '',
        'model_id' => '',
        'request_kwargs' => '{}',
        'exception_type' => '',
        'exception_string' => '',
        'status_code' => '',
    ];

    protected $casts = [
        'startTime' => 'datetime',
        'endTime' => 'datetime',
        'request_kwargs' => 'array',
    ];
}