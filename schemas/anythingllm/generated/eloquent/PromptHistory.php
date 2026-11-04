<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PromptHistory extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'modifiedAt';

    protected $table = 'prompt_history';

    protected $fillable = [
        'workspaceId',
        'prompt',
        'modifiedBy',
    ];

    protected $casts = [
        'workspaceId' => 'integer',
        'modifiedBy' => 'integer',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspaceId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'modifiedBy');
    }
}