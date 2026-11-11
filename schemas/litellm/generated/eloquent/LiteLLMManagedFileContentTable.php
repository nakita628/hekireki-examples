<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class LiteLLMManagedFileContentTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_ManagedFileContentTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'content',
    ];

    protected $casts = [
        'content' => AsBytes::class,
    ];
}