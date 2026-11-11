<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMClaudeCodePluginVersion extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ClaudeCodePluginVersion';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'plugin_id',
        'version',
        'description',
        'manifest_json',
        'published_at',
        'published_by',
        'archive_available',
        'content',
    ];

    protected $attributes = [
        'archive_available' => false,
    ];

    protected $casts = [
        'published_at' => 'datetime',
        'archive_available' => 'boolean',
        'content' => AsBytes::class,
    ];

    public function plugin(): BelongsTo
    {
        return $this->belongsTo(LiteLLMClaudeCodePluginTable::class, 'plugin_id');
    }
}