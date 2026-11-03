<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class MembershipInvitation extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'membership_invitations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'org_id',
        'org_role',
        'project_id',
        'project_role',
        'invited_by_user_id',
    ];

    protected $casts = [
        'org_role' => Role::class,
        'project_role' => Role::class,
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function invitedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'invited_by_user_id');
    }
}