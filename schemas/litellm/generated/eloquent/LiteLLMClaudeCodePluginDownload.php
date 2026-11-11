<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMClaudeCodePluginDownload extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ClaudeCodePluginDownload';

    public $timestamps = false;

    protected $fillable = [
        'plugin_id',
        'downloaded_at',
        'version',
        'digest',
        'authenticated',
        'user_id',
        'user_email',
        'key_alias',
        'team_id',
        'team_alias',
    ];

    protected $casts = [
        'downloaded_at' => 'datetime',
        'authenticated' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('downloaded_at', $this->attributes)) {
                $this->setAttribute('downloaded_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function plugin(): BelongsTo
    {
        return $this->belongsTo(LiteLLMClaudeCodePluginTable::class, 'plugin_id');
    }
}