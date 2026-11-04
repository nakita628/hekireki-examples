<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Users extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'users';

    protected $fillable = [
        'username',
        'password',
        'pfpFilename',
        'role',
        'suspended',
        'seen_recovery_codes',
        'lastUpdatedAt',
        'dailyMessageLimit',
        'bio',
        'web_push_subscription_config',
    ];

    protected $attributes = [
        'role' => 'default',
        'suspended' => 0,
        'seen_recovery_codes' => false,
        'bio' => '',
    ];

    protected $casts = [
        'suspended' => 'integer',
        'seen_recovery_codes' => 'boolean',
        'lastUpdatedAt' => 'datetime',
        'dailyMessageLimit' => 'integer',
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

    public function workspace_chats(): HasMany
    {
        return $this->hasMany(WorkspaceChats::class, 'user_id');
    }

    public function workspace_users(): HasMany
    {
        return $this->hasMany(WorkspaceUsers::class, 'user_id');
    }

    public function embed_configs(): HasMany
    {
        return $this->hasMany(EmbedConfigs::class, 'usersId');
    }

    public function embed_chats(): HasMany
    {
        return $this->hasMany(EmbedChats::class, 'usersId');
    }

    public function threads(): HasMany
    {
        return $this->hasMany(WorkspaceThreads::class, 'user_id');
    }

    public function recovery_codes(): HasMany
    {
        return $this->hasMany(RecoveryCodes::class, 'user_id');
    }

    public function password_reset_tokens(): HasMany
    {
        return $this->hasMany(PasswordResetTokens::class, 'user_id');
    }

    public function workspace_agent_invocations(): HasMany
    {
        return $this->hasMany(WorkspaceAgentInvocations::class, 'user_id');
    }

    public function slash_command_presets(): HasMany
    {
        return $this->hasMany(SlashCommandPresets::class, 'userId');
    }

    public function browser_extension_api_keys(): HasMany
    {
        return $this->hasMany(BrowserExtensionApiKeys::class, 'user_id');
    }

    public function temporary_auth_tokens(): HasMany
    {
        return $this->hasMany(TemporaryAuthTokens::class, 'userId');
    }

    public function system_prompt_variables(): HasMany
    {
        return $this->hasMany(SystemPromptVariables::class, 'userId');
    }

    public function prompt_history(): HasMany
    {
        return $this->hasMany(PromptHistory::class, 'modifiedBy');
    }

    public function desktop_mobile_devices(): HasMany
    {
        return $this->hasMany(DesktopMobileDevices::class, 'userId');
    }

    public function workspace_parsed_files(): HasMany
    {
        return $this->hasMany(WorkspaceParsedFiles::class, 'userId');
    }

    public function memories(): HasMany
    {
        return $this->hasMany(Memories::class, 'user_id');
    }
}