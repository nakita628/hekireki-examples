<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Workspaces extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspaces';

    protected $fillable = [
        'name',
        'slug',
        'vectorTag',
        'openAiTemp',
        'openAiHistory',
        'lastUpdatedAt',
        'openAiPrompt',
        'similarityThreshold',
        'chatProvider',
        'chatModel',
        'topN',
        'chatMode',
        'pfpFilename',
        'agentProvider',
        'agentModel',
        'queryRefusalResponse',
        'vectorSearchMode',
        'router_id',
    ];

    protected $attributes = [
        'openAiHistory' => 20,
        'similarityThreshold' => 0.25,
        'topN' => 4,
        'chatMode' => 'chat',
        'vectorSearchMode' => 'default',
    ];

    protected $casts = [
        'openAiTemp' => 'float',
        'openAiHistory' => 'integer',
        'lastUpdatedAt' => 'datetime',
        'similarityThreshold' => 'float',
        'topN' => 'integer',
        'router_id' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function workspace_users(): HasMany
    {
        return $this->hasMany(WorkspaceUsers::class, 'workspace_id');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(WorkspaceDocuments::class, 'workspaceId');
    }

    public function workspace_suggested_messages(): HasMany
    {
        return $this->hasMany(WorkspaceSuggestedMessages::class, 'workspaceId');
    }

    public function embed_configs(): HasMany
    {
        return $this->hasMany(EmbedConfigs::class, 'workspace_id');
    }

    public function threads(): HasMany
    {
        return $this->hasMany(WorkspaceThreads::class, 'workspace_id');
    }

    public function workspace_agent_invocations(): HasMany
    {
        return $this->hasMany(WorkspaceAgentInvocations::class, 'workspace_id');
    }

    public function prompt_history(): HasMany
    {
        return $this->hasMany(PromptHistory::class, 'workspaceId');
    }

    public function workspace_parsed_files(): HasMany
    {
        return $this->hasMany(WorkspaceParsedFiles::class, 'workspaceId');
    }

    public function memories(): HasMany
    {
        return $this->hasMany(Memories::class, 'workspace_id');
    }
}