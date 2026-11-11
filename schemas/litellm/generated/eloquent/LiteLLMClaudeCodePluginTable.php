<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class LiteLLMClaudeCodePluginTable extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_ClaudeCodePluginTable';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'version',
        'description',
        'manifest_json',
        'files_json',
        'enabled',
        'created_by',
        'anonymous_download_count',
    ];

    protected $attributes = [
        'files_json' => '{}',
        'enabled' => true,
        'anonymous_download_count' => 0,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'anonymous_download_count' => 'integer',
    ];

    public function uploaded_archive(): HasOne
    {
        return $this->hasOne(LiteLLMClaudeCodePluginUpload::class, 'plugin_name', 'name');
    }

    public function versions(): HasMany
    {
        return $this->hasMany(LiteLLMClaudeCodePluginVersion::class, 'plugin_id');
    }

    public function downloads(): HasMany
    {
        return $this->hasMany(LiteLLMClaudeCodePluginDownload::class, 'plugin_id');
    }
}