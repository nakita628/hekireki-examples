<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMCredentialsTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_CredentialsTable';

    protected $primaryKey = 'credential_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'credential_name',
        'display_name',
        'credential_values',
        'credential_info',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'credential_values' => 'array',
        'credential_info' => 'array',
    ];
}