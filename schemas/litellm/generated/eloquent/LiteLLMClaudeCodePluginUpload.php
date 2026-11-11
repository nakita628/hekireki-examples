<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMClaudeCodePluginUpload extends Model
{
    protected $table = 'LiteLLM_ClaudeCodePluginUpload';

    protected $primaryKey = 'plugin_name';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'plugin_name',
        'content',
    ];

    protected $casts = [
        'content' => AsBytes::class,
    ];

    public function plugin(): BelongsTo
    {
        return $this->belongsTo(LiteLLMClaudeCodePluginTable::class, 'plugin_name', 'name');
    }
}