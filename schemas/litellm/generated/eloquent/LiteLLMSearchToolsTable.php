<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMSearchToolsTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_SearchToolsTable';

    protected $primaryKey = 'search_tool_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'search_tool_name',
        'litellm_params',
        'search_tool_info',
    ];

    protected $casts = [
        'litellm_params' => 'array',
        'search_tool_info' => 'array',
    ];
}