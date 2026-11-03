<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SpaceMemberInvite extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'space_member_invites';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'space_id',
        'email',
        'role',
        'inviter_id',
    ];

    protected $attributes = [
        'role' => 'MEMBER',
    ];

    protected $casts = [
        'role' => SpaceMemberRole::class,
    ];

    public function invitedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'inviter_id');
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }
}