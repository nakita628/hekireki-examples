<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMUISettings extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_UISettings';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'ui_settings',
    ];

    protected $casts = [
        'ui_settings' => 'array',
    ];
}