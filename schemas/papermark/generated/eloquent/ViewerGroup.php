<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ViewerGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ViewerGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'domains',
        'allowAll',
        'dataroomId',
        'teamId',
    ];

    protected $attributes = [
        'allowAll' => false,
    ];

    protected $casts = [
        'allowAll' => 'boolean',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(ViewerGroupMembership::class, 'groupId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'groupId');
    }

    public function accessControls(): HasMany
    {
        return $this->hasMany(ViewerGroupAccessControls::class, 'groupId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'groupId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'viewerGroupId');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(ViewerInvitation::class, 'groupId');
    }

    public function questionAssignments(): HasMany
    {
        return $this->hasMany(DataroomQuestionAssignment::class, 'groupId');
    }

    public function taskAssignments(): HasMany
    {
        return $this->hasMany(TaskAssignment::class, 'groupId');
    }
}