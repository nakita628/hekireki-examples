<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ViewerInvitation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ViewerInvitation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'viewerId',
        'linkId',
        'groupId',
        'invitedBy',
        'customMessage',
        'sentAt',
        'status',
    ];

    protected $attributes = [
        'status' => 'SENT',
    ];

    protected $casts = [
        'sentAt' => 'datetime',
        'status' => InvitationStatus::class,
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('sentAt', $this->attributes)) {
                $this->setAttribute('sentAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }
}