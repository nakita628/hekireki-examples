<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SlackIntegration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'slack_integrations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'team_id',
        'team_name',
        'bot_token',
        'bot_user_id',
        'installer_user_id',
        'installer_slack_user_id',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}