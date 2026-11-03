<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SpaceMember extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'space_members';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'space_id',
        'user_id',
        'role',
        'last_selected_at',
    ];

    protected $attributes = [
        'role' => 'MEMBER',
    ];

    protected $casts = [
        'role' => SpaceMemberRole::class,
        'last_selected_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('last_selected_at', $this->attributes)) {
                $this->setAttribute('last_selected_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}