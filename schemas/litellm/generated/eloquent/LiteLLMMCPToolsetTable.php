<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMMCPToolsetTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_MCPToolsetTable';

    protected $primaryKey = 'toolset_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'toolset_name',
        'description',
        'tools',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'tools' => '[]',
    ];

    protected $casts = [
        'tools' => 'array',
    ];
}